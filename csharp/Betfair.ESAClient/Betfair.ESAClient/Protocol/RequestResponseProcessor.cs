using Betfair.ESAClient.Cache;
using Betfair.ESASwagger.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Betfair.ESAClient.Auth;

namespace Betfair.ESAClient.Protocol
{
    /// <summary>
    /// This class implements the basic request / response handling of the protocol
    /// </summary>
    public class RequestResponseProcessor
    {
        public const string OPERATION = "op";

        public const string REQUEST_AUTHENTICATION = "authentication";
        public const string REQUEST_MARKET_SUBSCRIPTION = "marketSubscription";
        public const string REQUEST_ORDER_SUBSCRIPTION = "orderSubscription";
        public const string REQUEST_HEARTBEAT = "heartbeat";

        public const string RESPONSE_CONNECTION = "connection";
        public const string RESPONSE_STATUS = "status";
        public const string RESPONSE_MARKET_CHANGE_MESSAGE = "mcm";
        public const string RESPONSE_ORDER_CHANGE_MESSAGE = "ocm";

        private int _nextId;
        private TaskCompletionSource<ConnectionMessage> _connectionMessage = new TaskCompletionSource<ConnectionMessage>();
        private ConcurrentDictionary<int, RequestResponse> _tasks = new ConcurrentDictionary<int, RequestResponse>();

        //subscription handlers
        private SubscriptionHandler<MarketSubscriptionMessage, ChangeMessage<MarketChange>, MarketChange> _marketSubscriptionHandler;
        private SubscriptionHandler<OrderSubscriptionMessage, ChangeMessage<OrderMarketChange>, OrderMarketChange> _orderSubscriptionHandler;

        private IChangeMessageHandler _changeHandler;
        private Action<string> _sendLine;
        private ConnectionStatus _status = ConnectionStatus.STOPPED;

        public DateTime LastRequestTime { get; private set; }
        public DateTime LastResponseTime { get; private set; }

        /// <summary>
        /// Lock used to syncronize the send process.
        /// </summary>
        private object _sendLock = new object();

        public RequestResponseProcessor(Action<string> sendLine)
        {
            _sendLine = sendLine;
            ChangeHandler = null;
            LastRequestTime = DateTime.MinValue;
            LastResponseTime = DateTime.MinValue;
        }

        public ConnectionStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (value == _status)
                {
                    //no-op
                    return;
                }
                ConnectionStatusEventArgs args = new ConnectionStatusEventArgs() { Old = _status, New = value };
                Trace.TraceInformation("ESAClient: Status changed {0} -> {1}", _status, value);
                _status = value;

                DispatchConnectionStatusChanged(args);
            }
        }

        private void DispatchConnectionStatusChanged(ConnectionStatusEventArgs args)
        {
            if(ConnectionStatusChanged != null)
            {
                ConnectionStatusChanged(this, args);
            }
        }

        /// <summary>
        /// Trace change messages but truncate to specified chars (0 is off).
        /// </summary>
        public int TraceChangeTruncation { get; set; }

        private void Reset()
        {
            _connectionMessage.TrySetCanceled();
            _connectionMessage = new TaskCompletionSource<ConnectionMessage>();
            foreach (RequestResponse task in _tasks.Values)
            {
                task.Cancelled();
            }
            _tasks = new ConcurrentDictionary<int, RequestResponse>();
        }

        public void Disconnected()
        {
            Status = ConnectionStatus.DISCONNECTED;
            Reset();
        }

        public void Stopped()
        {
            MarketSubscriptionHandler = null;
            OrderSubscriptionHandler = null;
            Status = ConnectionStatus.STOPPED;
            Reset();
        }

        public MarketSubscriptionMessage MarketResubscribeMessage
        {
            get
            {
                if(MarketSubscriptionHandler != null)
                {
                    MarketSubscriptionMessage resub = MarketSubscriptionHandler.SubscriptionMessage;
                    resub.InitialClk = MarketSubscriptionHandler.InitialClk;
                    resub.Clk = MarketSubscriptionHandler.Clk;
                    return resub;
                }
                return null;
            }
        }

        public OrderSubscriptionMessage OrderResubscribeMessage
        {
            get
            {
                if (OrderSubscriptionHandler != null)
                {
                    OrderSubscriptionMessage resub = OrderSubscriptionHandler.SubscriptionMessage;
                    resub.InitialClk = OrderSubscriptionHandler.InitialClk;
                    resub.Clk = OrderSubscriptionHandler.Clk;
                    return resub;
                }
                return null;
            }
        }


        public IChangeMessageHandler ChangeHandler
        {
            get
            {
                return _changeHandler;
            }
            set
            {
                if (_changeHandler == null)
                {
                    _changeHandler = new NullChangeHandler();
                } else
                {
                    _changeHandler = value;
                }
            }
        }

        public SubscriptionHandler<MarketSubscriptionMessage, ChangeMessage<MarketChange>, MarketChange> MarketSubscriptionHandler
        {
            get
            {
                return _marketSubscriptionHandler;
            }

            set
            {
                if (_marketSubscriptionHandler != null) _marketSubscriptionHandler.Cancel();
                _marketSubscriptionHandler = value;
                if(value != null) Status = ConnectionStatus.SUBSCRIBED;              
            }
        }

        public SubscriptionHandler<OrderSubscriptionMessage, ChangeMessage<OrderMarketChange>, OrderMarketChange> OrderSubscriptionHandler
        {
            get
            {
                return _orderSubscriptionHandler;
            }

            set
            {
                if (_orderSubscriptionHandler != null) _orderSubscriptionHandler.Cancel();
                _orderSubscriptionHandler = value;
                if (value != null) Status = ConnectionStatus.SUBSCRIBED;
            }
        }

        public Task<ConnectionMessage> ConnectionMessage()
        {
            return _connectionMessage.Task;
        }

        public Task<StatusMessage> Authenticate(AuthenticationMessage message)
        {
            message.Id = NextId();
            message.Op = REQUEST_AUTHENTICATION;
            return SendMessage(new RequestResponse((int)message.Id, message, 
                success => Status = ConnectionStatus.AUTHENTICATED));
        }

        public Task<StatusMessage> Heartbeat(HeartbeatMessage message)
        {
            message.Id = NextId();
            message.Op = REQUEST_HEARTBEAT;
            return SendMessage(new RequestResponse((int)message.Id, message, null));
        }

        public Task<StatusMessage> MarketSubscription(MarketSubscriptionMessage message)
        {
            int id = NextId();
            message.Id = id;
            message.Op = REQUEST_MARKET_SUBSCRIPTION;
            var newSub = new SubscriptionHandler<MarketSubscriptionMessage, ChangeMessage<MarketChange>, MarketChange>(id, message, false);
            return SendMessage(new RequestResponse(id, message,
                success => MarketSubscriptionHandler = newSub));
        }

        public Task<StatusMessage> OrderSubscription(OrderSubscriptionMessage message)
        {
            int id = NextId();
            message.Id = id;
            message.Op = REQUEST_ORDER_SUBSCRIPTION;
            var newSub = new SubscriptionHandler<OrderSubscriptionMessage, ChangeMessage<OrderMarketChange>, OrderMarketChange>(id, message, false);
            return SendMessage(new RequestResponse(id, message, 
                success => OrderSubscriptionHandler = newSub));
        }

        private int NextId()
        {
            int id = Interlocked.Increment(ref _nextId);
            return id;
        }


        private Task<StatusMessage> SendMessage(RequestResponse requestResponse)
        {
            lock (_sendLock)
            {
                //store a future task
                _tasks[requestResponse.Id] = requestResponse;

                //serialize message & send
                string line = JsonConvert.SerializeObject(requestResponse.Request, Formatting.None);
                Trace.TraceInformation("Client->ESA: " + line);

                //send line
                _sendLine(line);

                //time
                LastRequestTime = DateTime.UtcNow;
            }

            return requestResponse.Task;
        }

        /// <summary>
        /// Process a line of json
        /// </summary>
        /// <exception cref="JsonException">Thrown if line was invalid json</exception>
        /// <param name="line"></param>
        public ResponseMessage ReceiveLine(string line)
        {
            //clear last response
            ResponseMessage message = null;
            LastResponseTime = DateTime.UtcNow;
            var time = Stopwatch.StartNew();
            string operation = GetOperation(new JsonTextReader(new StringReader(line)));
            switch (operation)
            {
                case RESPONSE_CONNECTION:
                    Trace.TraceInformation("ESA->Client: {0}", line);
                    ConnectionMessage connectionMessage = ReadResponseMessage<ConnectionMessage>(line);
                    message = connectionMessage;
                    ProcessConnectionMessage(connectionMessage);
                    break;
                case RESPONSE_STATUS:
                    Trace.TraceInformation("ESA->Client: {0}", line);
                    StatusMessage statusMessage = ReadResponseMessage<StatusMessage>(line);
                    message = statusMessage;
                    ProcessStatusMessage(statusMessage);
                    break;
                case RESPONSE_MARKET_CHANGE_MESSAGE:
                    TraceChange(line);
                    MarketChangeMessage marketChangeMessage = ReadResponseMessage<MarketChangeMessage>(line);
                    message = marketChangeMessage;
                    ProcessMarketChangeMessage(marketChangeMessage);
                    break;
                case RESPONSE_ORDER_CHANGE_MESSAGE:
                    TraceChange(line);
                    OrderChangeMessage orderChangeMessage = ReadResponseMessage<OrderChangeMessage>(line);
                    message = orderChangeMessage;
                    ProcessOrderChangeMessage(orderChangeMessage);
                    break;
                default:
                    Trace.TraceError("ESA->Client: Unknown message type: {0}, message:{1}", operation, line);
                    break;
            }
            time.Stop();

            return message;
        }

        private void TraceChange(string line)
        {
            if (TraceChangeTruncation != 0)
            {
                Trace.TraceInformation("ESA->Client: {0}", line.Substring(0, Math.Min(TraceChangeTruncation, line.Length)));
            }              
        }

        private void ProcessOrderChangeMessage(OrderChangeMessage message)
        {
            ChangeMessage<OrderMarketChange> change = ChangeMessageFactory.ToChangeMessage(message);
            change = OrderSubscriptionHandler.ProcessChangeMessage(change);

            if (change != null) ChangeHandler.OnOrderChange(change);
        }

        private void ProcessMarketChangeMessage(MarketChangeMessage message)
        {
            ChangeMessage<MarketChange> change = ChangeMessageFactory.ToChangeMessage(message);
            change = MarketSubscriptionHandler.ProcessChangeMessage(change);

            if(change != null) ChangeHandler.OnMarketChange(change);
        }

        private void ProcessStatusMessage(StatusMessage statusMessage)
        {

            if (statusMessage.Id == null)
            {
                //async status / status for a message that couldn't be decoded
                ProcessUncorrelatedStatus(statusMessage);
            } else {
                RequestResponse task;
                _tasks.TryGetValue((int)statusMessage.Id, out task);
                if(task == null)
                {
                    //shouldn't happen
                    ProcessUncorrelatedStatus(statusMessage);
                } else
                {
                    //unwind task
                    task.ProcesStatusMessage(statusMessage);
                }
            }
        }

        public void ProcessUncorrelatedStatus(StatusMessage statusMessage)
        {
            Trace.TraceError("Error Status Notification: {0}", statusMessage);
            ChangeHandler.OnErrorStatusNotification(statusMessage);
        }

        protected void ProcessConnectionMessage(ConnectionMessage connectionMessage)
        {
            _connectionMessage.TrySetResult(connectionMessage);
            Status = ConnectionStatus.CONNECTED;
        }

        private T ReadResponseMessage<T>(string line) where T : ResponseMessage
        {
            T response = JsonConvert.DeserializeObject<T>(line);
            return response;
        }

        /// <summary>
        /// Extracts the op / operation attribute denoting the type of the message
        /// </summary>
        /// <param name="jreader">A reader containing json</param>
        /// <returns>The "op" value</returns>
        protected string GetOperation(JsonReader jreader)
        {
            string operation = null;
            if (jreader.Read())
            {
                //rip off start
                while (jreader.Read())
                {
                    if (jreader.TokenType == JsonToken.PropertyName && OPERATION.Equals(jreader.Value))
                    {
                        if (jreader.Read()) //rip out op's value
                        {
                            operation = (string)jreader.Value;
                        }
                        break;
                    }
                    else
                    {
                        jreader.Skip();
                    }
                }
            }
            return operation;
        }

        private class NullChangeHandler : IChangeMessageHandler
        {
            public void OnMarketChange(ChangeMessage<MarketChange> change)
            {
                Console.WriteLine("<null change handler>.OnMarketChange: " + change);
            }

            public void OnOrderChange(ChangeMessage<OrderMarketChange> change)
            {
                Console.WriteLine("<null change handler>.OnOrderChange: " + change);
            }

            public void OnErrorStatusNotification(StatusMessage message)
            {
                Console.WriteLine("<null change handler>.OnErrorStatusNotification: " + message);
            }
        }

        public event ConnectionStatusChangedEventHandler ConnectionStatusChanged;
    }

    public delegate void ConnectionStatusChangedEventHandler(object sender, ConnectionStatusEventArgs e);

    public class ConnectionStatusEventArgs : EventArgs
    {
        public ConnectionStatus Old { get; set; }
        public ConnectionStatus New { get; set; }
    }
}
