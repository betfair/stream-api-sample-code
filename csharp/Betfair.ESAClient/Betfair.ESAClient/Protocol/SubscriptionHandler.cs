using Betfair.ESASwagger.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Protocol
{
    /// <summary>
    /// Generic subscription handler for change messages:
    /// 1) Tracks clocks to facilitate resubscripiton
    /// 2) Provides useful timings for initial image
    /// 3) Supports the ability to re-combine segmented messages to retain event level atomicity
    /// </summary>
    /// <typeparam name="S">Subscription request message type</typeparam>
    /// <typeparam name="C">Change message type</typeparam>
    /// <typeparam name="I">Change message item type</typeparam>
    public class SubscriptionHandler<S,C,I> where C : ChangeMessage<I> where S : RequestMessage
    {
        private int _subscriptionId;
        private bool _isSubscribed;
        private bool _isMergeSegments;
        private List<I> _mergedChanges;
        private Stopwatch _ttfm;
        private Stopwatch _ttlm;
        private int _itemCount;
        private TaskCompletionSource<bool> _subscriptionComplete = new TaskCompletionSource<bool>();

        public SubscriptionHandler(int id, S subscriptionMessage, bool isMergeSegments)
        {
            SubscriptionMessage = subscriptionMessage;
            _isMergeSegments = isMergeSegments;
            IsSubscribed = false;
            _subscriptionId = id;
            _ttfm = Stopwatch.StartNew();
            _ttlm = Stopwatch.StartNew();
        }

        internal void Cancel()
        {
            //unwind waiters
            _subscriptionComplete.TrySetCanceled();
        }


        public string InitialClk { get; private set; }
        public string Clk { get; private set; }
        public S SubscriptionMessage { get; private set; }
        public bool IsSubscribed { get; private set; }
        public long? HeartbeatMs { get; private set; }
        public long? ConflationMs { get; private set; }
        public long? LastPt { get; private set; }
        public DateTime LastArrivalTime { get; private set; }
        public TimeSpan HeartbeatInterval { get; private set; }

        public Task<bool> SubscriptionComplete
        {
            get
            {
                return _subscriptionComplete.Task;
            }
        }


        public C ProcessChangeMessage(C changeMessage)
        {
            if(_subscriptionId != changeMessage.Id)
            {
                //previous subscription id - ignore
                return null;
            }

            //Every message store timings
            LastPt = changeMessage.Pt;
            LastArrivalTime = changeMessage.ArrivalTime;

            if (changeMessage.IsStartOfRecovery)
            {
                //Start of recovery
                _ttfm.Stop();
                Trace.TraceInformation("{0}: Start of image", typeof(S).Name);
            }

            if (changeMessage.ChangeType == ChangeType.HEARTBEAT)
            {
                //Swallow heartbeats
                changeMessage = null;
            }
            else if(changeMessage.SegmentType != SegmentType.NONE && _isMergeSegments)
            {
                //Segmented message and we're instructed to merge (which makes segments look atomic).
                changeMessage = MergeMessage(changeMessage);
            }

            if(changeMessage != null)
            {
                //store clocks
                if(changeMessage.InitialClk != null)
                {
                    InitialClk = changeMessage.InitialClk;
                }
                if(changeMessage.Clk != null)
                {
                    Clk = changeMessage.Clk;
                }

                if (!_isSubscribed)
                {
                    //During recovery
                    if (changeMessage.Items != null)
                    {
                        _itemCount += changeMessage.Items.Count;
                    }
                }
                
                if (changeMessage.IsEndOfRecovery)
                {
                    //End of recovery
                    _isSubscribed = true;
                    HeartbeatMs = changeMessage.HeartbeatMs;
                    HeartbeatInterval = TimeSpan.FromMilliseconds((double)HeartbeatMs);
                    ConflationMs = changeMessage.ConflateMs;
                    _ttlm.Stop();
                    Trace.TraceInformation("{0}: End of image: type:{6}, ttfm:{1}, ttlm:{2}, conflation:{3}, heartbeat:{4}, change.items:{5}", 
                        typeof(S).Name, 
                        _ttfm.Elapsed, 
                        _ttlm.Elapsed, 
                        ConflationMs, 
                        HeartbeatMs,
                        _itemCount,
                        changeMessage.ChangeType);

                    //unwind future
                    _subscriptionComplete.TrySetResult(true);
                }
               
            }
            return changeMessage;
        }

        private C MergeMessage(C changeMessage)
        {
            //merge segmented messages so client sees atomic view across segments
            if (changeMessage.SegmentType == SegmentType.SEG_START)
            {
                //start merging
                _mergedChanges = new List<I>();
            }
            //accumulate
            _mergedChanges.AddRange(changeMessage.Items);

            if (changeMessage.SegmentType == SegmentType.SEG_END)
            {
                //finish merging
                changeMessage.SegmentType = SegmentType.NONE;
                changeMessage.Items = _mergedChanges;
                _mergedChanges = null;
            }
            else
            {
                //swallow message as we're still merging
                changeMessage = null;
            }
            return changeMessage;
        }
    }
}
