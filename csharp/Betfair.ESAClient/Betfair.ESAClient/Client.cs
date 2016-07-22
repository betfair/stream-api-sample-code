
using Betfair.ESAClient.Auth;
using Betfair.ESAClient.Protocol;
using Betfair.ESASwagger.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Betfair.ESAClient
{
    /// <summary>
    /// Simple ESA Client that connects using a standard SSL socket.
    /// This client implements blocking semantics and AutoReconnect.
    /// </summary>
    public class Client
    {
        //socket members
        private StreamReader _reader;
        private StreamWriter _writer;
        private string _hostName;
        private int _port;
        private TcpClient _client;

        /// <summary>
        /// Handles session creation
        /// </summary>
        private AppKeyAndSessionProvider _sessionProvider;
        /// <summary>
        /// Handles request / response correlation
        /// </summary>
        private RequestResponseProcessor _processor;

        /// <summary>
        /// Timer that checks socket connectivity.
        /// </summary>
        private Timer _keepAliveTimer;

        /// <summary>
        /// Lock protecting start / stop flow control
        /// </summary>
        private readonly object _startStopLock = new object();
        /// <summary>
        /// Lock used during retry back-off
        /// </summary>
        private readonly object _retryLock = new object();
        /// <summary>
        /// Flag used to protect / signal starting.
        /// </summary>
        private volatile bool _isStarted = false;
        /// <summary>
        /// Flag used to protect / signal stopping.
        /// </summary>
        private volatile bool _isStopping = false;
        /// <summary>
        /// Signal send when stopping is completed
        /// </summary>
        private ManualResetEvent _isStopped;

        /// <summary>
        /// Count of disconnects.
        /// </summary>
        public int DisconnectCounter { get; private set; }
        /// <summary>
        /// Count of reconnects (reset once reconnect completes).
        /// </summary>
        public int ReconnectCounter { get; private set; }


        /// <summary>
        /// Conflation rate to set on subscriptions (if set)
        /// </summary>
        public long? ConflateMs { get; set; }
        /// <summary>
        /// Heartbeat rate to set on subscriptions (if set)
        /// </summary>
        public long? HeartbeatMs { get; set; }
        /// <summary>
        /// Default market data filter (if set) to set on every market 
        /// data subscription.
        /// </summary>
        public MarketDataFilter MarketDataFilter { get; set; }



        /// <summary>
        /// Specifies the timeout (default is 30s)
        /// </summary>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// Specifies the connection retry back-off (default is 15s)
        /// Which kicks in after the first attempt.
        /// </summary>
        public TimeSpan ReconnectBackOff { get; set; }

        /// <summary>
        /// Specifies a time to send an explicit hearbeat (default is 1hr)
        /// </summary>
        public TimeSpan KeepAliveHeartbeat { get; set; }

        /// <summary>
        /// Connection status
        /// </summary>
        public ConnectionStatus Status {
            get
            {
                return _processor.Status;
            }
        }


        /// <summary>
        /// Construct a new client to the specified host and port
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="port"></param>
        /// <param name="sessionProvider"></param>
        public Client(string hostName, int port, AppKeyAndSessionProvider sessionProvider)
        {
            _hostName = hostName;
            _port = port;
            _sessionProvider = sessionProvider;
            _processor = new RequestResponseProcessor(SendLine);
            _processor.ConnectionStatusChanged += DispatchConnectionStatusChanged;

            //default properties
            AutoReconnect = true;
            Timeout = TimeSpan.FromSeconds(30);
            ReconnectBackOff = TimeSpan.FromSeconds(15);
            KeepAliveHeartbeat = TimeSpan.FromHours(1);
        }

        private void DispatchConnectionStatusChanged(object sender, ConnectionStatusEventArgs e)
        {
            if(ConnectionStatusChanged != null)
            {
                try
                {
                    ConnectionStatusChanged(this, e);
                } catch (Exception ex)
                {
                    Trace.TraceError("Exception thrown dispatch", ex);
                }
            }
        }

        /// <summary>
        /// Event raised on connection status change.
        /// </summary>
        public event ConnectionStatusChangedEventHandler ConnectionStatusChanged;

        /// <summary>
        /// Whether to automatically re-connect and recover any active subscriptions.
        /// Note:
        /// 1) If subscription completed it will be recovered
        /// 2) Subscription recovery is via delta
        /// </summary>
        public bool AutoReconnect { get; set; }

        /// <summary>
        /// Last connect time
        /// </summary>
        public DateTime LastConnectTime { get; private set; }

        /// <summary>
        /// Wether to log change messages and if so at what character trunctation 
        /// (default is 0 which is off)
        /// </summary>
        public int TraceChangeTruncation
        {
            get
            {
                return _processor.TraceChangeTruncation;                
            }
            set
            {
                _processor.TraceChangeTruncation = value;
            }
        }

        /// <summary>
        /// ClientCache is abstracted via this hook (enabling replacement)
        /// </summary>
        public IChangeMessageHandler ChangeHandler
        {
            get
            {
                return _processor.ChangeHandler;
            }
            set
            {
                _processor.ChangeHandler = value;
            }
        }


        /// <summary>
        /// Starts a connection (synchronously).
        /// </summary>
        public void Start()
        {
            lock (_startStopLock)
            {
                //signalling
                if (_isStarted)
                {
                    //already started
                    return;
                }
                _isStopping = false;
                _isStopped = new ManualResetEvent(false);

                //Reset disconnect counter
                DisconnectCounter = 0;

                //On initial start ensure we have a fresh session (to avoid risk of invalid session).
                _sessionProvider.ExpireTokenNow();


                //Socket level connect
                ConnectSocket();

                //Start processing thread
                Thread thread = new Thread(new ThreadStart(Run));
                thread.Name = "ESAClient";
                thread.Start();

                //Start keep alive timer
                _keepAliveTimer = new Timer(KeepAliveCheck, null, Timeout, Timeout);

                ConnectAndAuthenticate();

                //got to here so we are started
                _isStarted = true;
            }
        }



        public void Stop()
        {
            lock (_startStopLock)
            {
                if (!_isStarted)
                {
                    //already stopped
                    return;
                }

                //signal ESA client thread to stop
                _isStopping = true;

                //shutdown keep-alive
                _keepAliveTimer.Dispose();

                //signal in case sleeping    
                lock (_retryLock)
                {
                    Monitor.PulseAll(_retryLock);
                }

                //shutdown socket
                Disconnect();

                //block waiting for esa client thread to signal exit
                _isStopped.WaitOne();
            }
        }

        /// <summary>
        /// Force disconnects the socket.
        /// Note: 
        /// If AutoReconnect is enabled the socket will be re-created.
        /// </summary>
        public void Disconnect()
        {
            //Disconnect socket
            if (_client != null)
            {
                _client.Close();
                _client = null;
            }
        }

        private void KeepAliveCheck(object state)
        {
            try
            {
                if(_processor.Status == ConnectionStatus.SUBSCRIBED)
                {
                    //connection looks up
                    if (_processor.LastRequestTime + KeepAliveHeartbeat < DateTime.UtcNow)
                    {
                        //send a heartbeat to server to keep networks open
                        Trace.TraceInformation("Last Request Time is longer than {0}: Sending Keep Alive Heartbate", KeepAliveHeartbeat);
                        Heartbeat();
                    }
                    else if(_processor.LastResponseTime + Timeout < DateTime.UtcNow)
                    {
                        Trace.TraceInformation("Last Response Time is longer than {0}: Sending Keep Alive Heartbate", Timeout);
                        Heartbeat();
                    }

                }
            }
            catch (Exception e)
            {
                Trace.TraceError("Keep alive failed", e);
            }
        }

        private void ConnectAndAuthenticate()
        {
            //Wait for connection message
            if (!_processor.ConnectionMessage().Wait(Timeout))
            {
                //timeout
                throw new TimeoutException("Request timeout - Timeout=" + Timeout);
            }

            //Authenticate
            Authenticate();
        }

        private void Authenticate()
        {
            //get a session
            AppKeyAndSession appKeyAndSession = _sessionProvider.GetOrCreateNewSession();
            try
            {
                WaitFor(_processor.Authenticate(new AuthenticationMessage()
                {
                    AppKey = appKeyAndSession.AppKey,
                    Session = appKeyAndSession.Session
                }));
            }
            catch (StatusException statusException)
            {
                //force expire if session is invalid
                if (statusException.ErrorCode == StatusMessage.ErrorCodeEnum.InvalidSessionInformation)
                {
                    _sessionProvider.ExpireTokenNow();
                }
                else
                {
                    throw statusException;
                }
            }
        }

        private Task<StatusMessage> WaitFor(Task<StatusMessage> task)
        {
            if (task.Wait(Timeout))
            {
                if (task.IsCanceled)
                {
                    throw new IOException("Connection failed");
                }
                //server responed
                if(task.Result.StatusCode == StatusMessage.StatusCodeEnum.Success)
                {
                    return task;
                }
                else
                {
                    //status error
                    throw new StatusException(task.Result);
                }
            }
            else
            {
                //timeout 
                throw new TimeoutException("Request timeout - Timeout=" + Timeout);
            }
        }



        /// <summary>
        /// Disconnects previous socket and then:
        /// 1) Creates a session.
        /// 2) Creates a socket.
        /// 3) Forms SSL layer.
        /// </summary>
        private void ConnectSocket()
        {
            LastConnectTime = DateTime.UtcNow;

            //Disconnect socket
            Disconnect();

            //pre-fetch the session (as auth will follow)
            AppKeyAndSession appKeyAndSession = _sessionProvider.GetOrCreateNewSession();

            //create socket
            Trace.TraceInformation("ESAClient: Opening socket to: {0}:{1}",_hostName, _port);
            _client = new TcpClient(_hostName, _port);
            _client.ReceiveBufferSize = 1024 * 1000 * 2; //shaves about 20s off firehose image.
            _client.SendTimeout = (int)Timeout.TotalMilliseconds;
            _client.ReceiveTimeout = (int)Timeout.TotalMilliseconds;
            Stream stream = _client.GetStream();

            if (_port == 443)
            {
                //SSL is on
                Trace.TraceInformation("ESAClient: Opening ssl stream to: {0}:{1}", _hostName, _port);

                // Create an SSL stream that will close the client's stream.
                SslStream sslStream = new SslStream(stream, false);

                //Setup ssl
                sslStream.AuthenticateAsClient(_hostName);

                stream = sslStream;
            }

            //Setup reader / writer
            _reader = new StreamReader(stream, Encoding.UTF8, false, _client.ReceiveBufferSize);
            _writer = new StreamWriter(stream, Encoding.UTF8);
        }




        private void Run()
        {
            Trace.TraceInformation("ESAClient: Processing thread started");
            while (!_isStopping)
            {
                string line = null;
                try
                {
                    line = _reader.ReadLine();
                    if(line == null)
                    {
                        throw new IOException("Socket closed - EOF");
                    }
                    else
                    {
                        _processor.ReceiveLine(line);
                    }
                }
                catch (Exception e)
                {
                    if (!_isStopping)
                    {
                        Trace.TraceError("Error received processing socket - disconnecting: {0}", e);
                    }
                    Disconnected();
                }
            }
            _isStopped.Set();
            _isStarted = false;
            Trace.TraceWarning("ESAClient: Processing thread stopped");
        }

        private void Disconnected()
        {
            DisconnectCounter++;

            
            if (_isStarted && !_isStopping && AutoReconnect)
            {
                //we've started (successfully) and we're not stopping & should autoreconnect

                //disconnected
                _processor.Disconnected();

                //try and reconnect
                TryReconnect();
            }
            else
            {
                //stopped
                _processor.Stopped();

                //no reconnect
                _isStopping = true;
            }
        }



        private void TryReconnect()
        {
            if(DateTime.UtcNow - LastConnectTime < ReconnectBackOff)
            {
                //Not first disconnect
                lock (_retryLock)
                {
                    Trace.TraceInformation("Reconnect backoff for {0}ms", ReconnectBackOff);
                    Monitor.Wait(_retryLock, ReconnectBackOff);
                }
            }

            lock (_startStopLock)
            {
                if (_isStopping)
                {
                    //flag off isStopping & within lock to 
                    return;
                }

                try
                {
                    //create new connection to server - might fail
                    ConnectSocket();

                    //connect and authenticate - do this async
                    //as TryReconnect is called from run loop
                    Task.Run(() => ConnectAndAuthenticateAndResubscribe());
                }
                catch (Exception e)
                {
                    Trace.TraceError("Reconnect attempt={0} failed: ", ReconnectCounter, e);
                    ReconnectCounter++;
                }
            }   
        }

        private void ConnectAndAuthenticateAndResubscribe()
        {
            try
            {
                //Connect and auth
                ConnectAndAuthenticate();

                //Resub markets
                MarketSubscriptionMessage marketSubscription = _processor.MarketResubscribeMessage;
                if (marketSubscription != null)
                {
                    Trace.TraceInformation("Resubscribe to market subscription.");
                    MarketSubscription(marketSubscription);
                }

                //Resub orders
                OrderSubscriptionMessage orderSubscription = _processor.OrderResubscribeMessage;
                if (orderSubscription != null)
                {
                    Trace.TraceInformation("Resubscribe to order subscription.");
                    OrderSubscription(orderSubscription);
                }

                //Reset counter
                ReconnectCounter = 0;
            } catch (Exception e)
            {
                Trace.TraceError("Reconnect failed", e);
                ReconnectCounter++;
            }
        }

        /// <summary>
        /// Sends a request line to the server
        /// </summary>
        /// <param name="line"></param>
        private void SendLine(string line)
        {
            try
            {
                _writer.WriteLine(line);
                _writer.Flush();
            }
            catch(IOException e)
            {
                Trace.TraceError("Error sending to socket - disconnecting", e);
                //Foceably break the socket which will then trigger a reconnect if configured
                Disconnect();
                throw e;
            }
        }

        /// <summary>
        /// Subscribe the the specified orders (syncronously) with the configured 
        /// HeartbeatMS, ConflationMS.
        /// </summary>
        /// <param name="message"></param>
        public void OrderSubscription(OrderSubscriptionMessage message)
        {
            message.ConflateMs = ConflateMs;
            message.HeartbeatMs = HeartbeatMs;
            WaitFor(_processor.OrderSubscription(message));
        }

        /// <summary>
        /// Subscribe the the specified orders (syncronously) with the configured 
        /// HeartbeatMS, ConflationMS and MarketDataFilter.
        /// </summary>
        /// <param name="message"></param>
        public void MarketSubscription(MarketSubscriptionMessage message)
        {
            message.ConflateMs = ConflateMs;
            message.HeartbeatMs = HeartbeatMs;
            if (MarketDataFilter != null) message.MarketDataFilter = MarketDataFilter;
            WaitFor(_processor.MarketSubscription(message));
        }

        /// <summary>
        /// Heartbeat to the server (syncronously) - useful to keep network hardware open.
        /// (not required by server).
        /// </summary>
        public void Heartbeat()
        {
            WaitFor(_processor.Heartbeat(new HeartbeatMessage()));
        }

    }


}
