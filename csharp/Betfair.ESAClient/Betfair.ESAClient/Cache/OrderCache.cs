using Betfair.ESAClient.Protocol;
using Betfair.ESASwagger.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Cache
{
    public class OrderCache
    {
        private readonly ConcurrentDictionary<string, OrderMarket> _markets = new ConcurrentDictionary<string, OrderMarket>();

        public OrderCache()
        {
            IsOrderMarketRemovedOnClose = true;
        }

        public void OnOrderChange(ChangeMessage<OrderMarketChange> changeMessage)
        {
            if (changeMessage.IsStartOfNewSubscription)
            {
                //clear cache
                _markets.Clear();
            }
            if(changeMessage.Items != null)
            {
                //lazy build events
                List<OrderMarketChangedEventArgs> batch = BatchOrderMarketsChanged == null ? null : new List<OrderMarketChangedEventArgs>(changeMessage.Items.Count);

                foreach (OrderMarketChange marketChange in changeMessage.Items)
                {
                    bool isImage = marketChange.FullImage == true;
                    if (isImage) {
                        // Clear market from cache if it is being re-imaged
                        OrderMarket removed;
                        _markets.TryRemove(marketChange.Id, out removed);
                    }

                    OrderMarket market = OnOrderMarketChange(marketChange);

                    if(IsOrderMarketRemovedOnClose && market.IsClosed)
                    {
                        //remove on close
                        OrderMarket removed;
                        _markets.TryRemove(market.MarketId, out removed);
                    }
                    //lazy build events
                    if (batch != null || OrderMarketChanged != null)
                    {
                        OrderMarketChangedEventArgs arg = new OrderMarketChangedEventArgs() { Change = marketChange, OrderMarket = market };
                        if (OrderMarketChanged != null)
                        {
                            DispatchOrderMarketChanged(arg);
                        }
                        if (batch != null)
                        {
                            batch.Add(arg);
                        }
                    }
                }
                if (batch != null)
                {
                    DispatchBatchOrderMarketsChanged(new BatchOrderMarketsChangedEventArgs() { Changes = batch });
                }
            }
        }

        private OrderMarket OnOrderMarketChange(OrderMarketChange marketChange)
        {
            OrderMarket market = _markets.GetOrAdd(marketChange.Id, id => new OrderMarket(this, id));
            market.OnOrderMarketChange(marketChange);
            return market;
        }

        private void DispatchOrderMarketChanged(OrderMarketChangedEventArgs args)
        {
            try
            {
                OrderMarketChanged.Invoke(this, args);
            }
            catch (Exception e)
            {
                Trace.TraceError("Error dispatching event: {0}", e);
            }
        }

        private void DispatchBatchOrderMarketsChanged(BatchOrderMarketsChangedEventArgs args)
        {
            try
            {
                BatchOrderMarketsChanged.Invoke(this, args);
            }
            catch (Exception e)
            {
                Trace.TraceError("Error dispatching event: {0}", e);
            }
        }

        /// <summary>
        /// Wether order markets are automatically removed on close
        /// (default is true)
        /// </summary>
        public bool IsOrderMarketRemovedOnClose { get; set; }

        /// <summary>
        /// Event for each order market change
        /// </summary>
        public event OrderMarketChangedEventHandler OrderMarketChanged;

        /// <summary>
        /// Event for each batch of order market changes
        /// (note to be truly atomic you will want to set to merge segments
        /// otherwise an event could be segmented)
        /// </summary>
        public event BatchOrderMarketsChangedEventHandler BatchOrderMarketsChanged;

        /// <summary>
        /// Queries by market id - the result is invariant for the 
        /// lifetime of the market.
        /// </summary>
        /// <param name="marketid"></param>
        /// <returns></returns>
        public OrderMarket this[string marketId]
        {
            get
            {
                return _markets[marketId];
            }
        }

        /// <summary>
        /// All the cached markets
        /// </summary>
        public IEnumerable<OrderMarket> Markets
        {
            get
            {
                return _markets.Values;
            }
        }

        /// <summary>
        /// Market count
        /// </summary>
        public int Count
        {
            get
            {
                return _markets.Count;
            }
        }
    }

    public delegate void OrderMarketChangedEventHandler(object sender, OrderMarketChangedEventArgs e);

    public delegate void BatchOrderMarketsChangedEventHandler(object sender, BatchOrderMarketsChangedEventArgs e);

    public class OrderMarketChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The raw change message that was just applied
        /// </summary>
        public OrderMarketChange Change {get; internal set;}

        /// <summary>
        /// The order market changed - this is reference invariant
        /// </summary>
        public OrderMarket OrderMarket { get; internal set; }

        /// <summary>
        /// Takes or returns an existing immutable snap of the market.
        /// </summary>
        public OrderMarketSnap Snap
        {
            get
            {
                return OrderMarket.Snap;
            }
        }
    }

    public class BatchOrderMarketsChangedEventArgs : EventArgs
    {
        public IList<OrderMarketChangedEventArgs> Changes { get; internal set; }
    }
}
