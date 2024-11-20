package com.betfair.esa.client;

import com.betfair.esa.client.auth.AppKeyAndToken;
import com.betfair.esa.client.auth.AppKeyAndSessionProvider;
import com.betfair.esa.client.auth.InvalidCredentialException;
import com.betfair.esa.client.protocol.ChangeMessageHandler;
import com.betfair.esa.client.protocol.ConnectionException;
import com.betfair.esa.client.protocol.ConnectionStatus;
import com.betfair.esa.client.protocol.ConnectionStatusListener;
import com.betfair.esa.client.protocol.FutureResponse;
import com.betfair.esa.client.protocol.RequestResponseProcessor;
import com.betfair.esa.client.protocol.StatusException;
import com.betfair.esa.swagger.model.AuthenticationMessage;
import com.betfair.esa.swagger.model.ConnectionMessage;
import com.betfair.esa.swagger.model.HeartbeatMessage;
import com.betfair.esa.swagger.model.MarketDataFilter;
import com.betfair.esa.swagger.model.MarketSubscriptionMessage;
import com.betfair.esa.swagger.model.OrderSubscriptionMessage;
import com.betfair.esa.swagger.model.StatusMessage;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.Socket;
import java.util.concurrent.CancellationException;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;
import javax.net.SocketFactory;
import javax.net.ssl.SSLSocket;
import javax.net.ssl.SSLSocketFactory;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/** Created by mulveyj on 07/07/2016. */
public class Client {
    private static final Logger LOG = LoggerFactory.getLogger(Client.class);
    private static final String CRLF = "\r\n";

    private final String hostName;
    private final int port;
    private final AppKeyAndSessionProvider sessionProvider;
    private final RequestResponseProcessor processor;

    private ChangeMessageHandler changeHandler;

    private boolean autoReconnect = true;
    private long timeout = 30 * 1000;
    private long reconnectBackOff = 15 * 1000;
    private long keepAliveHeartbeat = 60 * 60 * 1000;

    // Threading control
    private final Object startStopLock = new Object();
    private boolean isStarted;
    private boolean isStopping;
    private CountDownLatch isStopped;
    private int disconnectCounter;
    private int reconnectCounter;
    private long lastConnectTime;
    private ScheduledExecutorService keepAliveTimer;
    private final Object retryLock = new Object();

    // IO
    private Socket client;
    private BufferedReader reader;
    private BufferedWriter writer;

    private Long conflateMs;
    private Long heartbeatMs;
    private MarketDataFilter marketDataFilter;

    public Client(String hostName, int port, AppKeyAndSessionProvider sessionProvider) {
        this.hostName = hostName;
        this.port = port;
        this.sessionProvider = sessionProvider;
        processor = new RequestResponseProcessor(line -> sendLineImpl(line));
    }

    public ChangeMessageHandler getChangeHandler() {
        return processor.getChangeHandler();
    }

    /**
     * ClientCache is abstracted via this hook (enabling replacement)
     *
     * @param changeHandler
     */
    public void setChangeHandler(ChangeMessageHandler changeHandler) {
        processor.setChangeHandler(changeHandler);
    }

    /** Starts a connection (synchronously). */
    public void start() throws InvalidCredentialException, ConnectionException, StatusException {
        synchronized (startStopLock) {
            // signalling
            if (isStarted) {
                // already started
                return;
            }
            isStopping = false;
            isStopped = new CountDownLatch(1);

            // Reset disconnect counter
            disconnectCounter = 0;

            // On initial start ensure we have a fresh session (to avoid risk of invalid session).
            sessionProvider.expireTokenNow();

            // Socket level connect
            connectSocket();

            // Start processing thread
            Thread thread = new Thread(this::run, "ESAClient");
            thread.start();

            // Start keep alive timer
            keepAliveTimer = Executors.newSingleThreadScheduledExecutor();
            keepAliveTimer.scheduleAtFixedRate(
                    this::keepAliveCheck, timeout, timeout, TimeUnit.MILLISECONDS);

            connectAndAuthenticate();

            // got to here so we are started
            isStarted = true;
        }
    }

    public void stop() {
        synchronized (startStopLock) {
            if (!isStarted) {
                // already stopped
                return;
            }

            // signal ESA client thread to stop
            isStopping = true;

            // shutdown keep-alive
            keepAliveTimer.shutdown();

            // signal in case sleeping
            synchronized (retryLock) {
                retryLock.notifyAll();
            }

            // shutdown socket
            disconnect();

            // block waiting for esa client thread to signal exit
            try {
                isStopped.await();
            } catch (InterruptedException e) {
                // not expected
                throw new RuntimeException(e);
            }
        }
    }

    /// <summary>
    /// Force disconnects the socket.
    /// Note:
    /// If AutoReconnect is enabled the socket will be re-created.
    /// </summary>

    /**
     * Force disconnects the socket. Note: If AutoReconnect is enabled the socket will be
     * re-created.
     */
    public void disconnect() {
        // Disconnect socket
        if (client != null) {
            try {
                client.close();
            } catch (IOException e) {
                LOG.warn("Unable to close socket", e);
            }
            client = null;
        }
    }

    private void keepAliveCheck() {
        try {
            if (processor.getStatus() == ConnectionStatus.SUBSCRIBED) {
                // connection looks up
                if (processor.getLastRequestTime() + keepAliveHeartbeat
                        < System.currentTimeMillis()) {
                    // send a heartbeat to server to keep networks open
                    LOG.info(
                            "Last Request Time is longer than {}: Sending Keep Alive Heartbate",
                            keepAliveHeartbeat);
                    heartbeat();
                } else if (processor.getLastResponseTime() + timeout < System.currentTimeMillis()) {
                    LOG.info(
                            "Last Response Time is longer than {}: Sending Keep Alive Heartbate",
                            timeout);
                    heartbeat();
                }
            }
        } catch (Exception e) {
            LOG.error("Keep alive failed", e);
        }
    }

    private void connectAndAuthenticate()
            throws ConnectionException, StatusException, InvalidCredentialException {
        // Wait for connection message
        try {
            ConnectionMessage result =
                    processor.getConnectionMessage().get(timeout, TimeUnit.MILLISECONDS);
            if (result == null) {
                // timeout
                throw new ConnectionException("No connection message");
            }
        } catch (Throwable e) {
            throw new ConnectionException("Connection message failed", e);
        }

        // Authenticate
        authenticate();
    }

    private void authenticate()
            throws InvalidCredentialException, ConnectionException, StatusException {
        // get a session
        AppKeyAndToken appKeyAndToken = null;
        try {
            appKeyAndToken = sessionProvider.getOrCreateNewSession();
        } catch (IOException e) {
            throw new ConnectionException("Authentication provider failed", e);
        }
        try {
            AuthenticationMessage authenticationMessage = new AuthenticationMessage();
            authenticationMessage.setAppKey(appKeyAndToken.getAppKey());
            authenticationMessage.setSession(appKeyAndToken.getToken());
            waitFor(processor.authenticate(authenticationMessage));
        } catch (StatusException statusException) {
            // force expire if session is invalid
            if (statusException.getErrorCode()
                    == StatusMessage.ErrorCodeEnum.INVALID_SESSION_INFORMATION) {
                sessionProvider.expireTokenNow();
            } else {
                throw statusException;
            }
        }
    }

    private FutureResponse<StatusMessage> waitFor(FutureResponse<StatusMessage> task)
            throws StatusException, ConnectionException {
        StatusMessage statusMessage = null;
        try {
            statusMessage = task.get(timeout, TimeUnit.MILLISECONDS);
            if (statusMessage != null) {
                // server responed
                if (statusMessage.getStatusCode() == StatusMessage.StatusCodeEnum.SUCCESS) {
                    return task;
                } else {
                    // status error
                    throw new StatusException(statusMessage);
                }
            } else {
                throw new ConnectionException("Result was expected");
            }

        } catch (InterruptedException e) {
            throw new ConnectionException("Future failed:", e);
        } catch (ExecutionException e) {
            throw new ConnectionException("Future failed:", e.getCause());
        } catch (CancellationException e) {
            throw new ConnectionException("Connection failed", e);
        } catch (TimeoutException e) {
            throw new ConnectionException("Future failed: (timeout)", e);
        }
    }

    /**
     * Disconnects previous socket and then: 1) Creates a session. 2) Creates a socket. 3) Forms SSL
     * layer.
     */
    private void connectSocket() throws ConnectionException, InvalidCredentialException {
        // last connect time
        lastConnectTime = System.currentTimeMillis();

        try {
            // Disconnect socket
            disconnect();

            // pre-fetch the session (as auth will follow)
            AppKeyAndToken appKeyAndToken = sessionProvider.getOrCreateNewSession();

            // create socket
            LOG.info("ESAClient: Opening socket to: {}:{}", hostName, port);
            client = createSocket(hostName, port);
            client.setReceiveBufferSize(1024 * 1000 * 2); // shaves about 20s off firehose image.
            client.setSoTimeout((int) timeout);

            // Setup reader / writer
            reader = new BufferedReader(new InputStreamReader(client.getInputStream()));
            writer = new BufferedWriter(new OutputStreamWriter(client.getOutputStream()));
        } catch (IOException e) {
            throw new ConnectionException("Failed to connect", e);
        }
    }

    private Socket createSocket(String hostName, int port) throws IOException {
        if (port == 443) {
            // ssl
            SocketFactory factory = SSLSocketFactory.getDefault();
            SSLSocket newSocket = (SSLSocket) factory.createSocket(hostName, port);
            newSocket.startHandshake();
            return newSocket;
        } else {
            // non-ssl
            return new Socket(hostName, port);
        }
    }

    private void run() {
        LOG.info("ESAClient: Processing thread started");
        while (!isStopping) {
            String line = null;
            try {
                line = reader.readLine();
                if (line == null) {
                    throw new IOException("Socket closed - EOF");
                } else {
                    processor.receiveLine(line);
                }
            } catch (Exception e) {
                if (!isStopping) {
                    LOG.error("ESAClient: Error received processing socket - disconnecting:", e);
                }
                disconnected();
            }
        }
        isStopped.countDown();
        isStarted = false;
        LOG.warn("ESAClient: Processing thread stopped");
    }

    private void disconnected() {
        disconnectCounter++;

        if (isStarted && !isStopping && autoReconnect) {
            // we've started (successfully) and we're not stopping & should autoreconnect

            // disconnected
            processor.disconnected();

            // try and reconnect
            tryReconnect();
        } else {
            // stopped
            processor.stopped();

            // no reconnect
            isStopping = true;
        }
    }

    private void tryReconnect() {
        if (System.currentTimeMillis() - lastConnectTime < reconnectBackOff) {
            // Not first disconnect & last disconnect was within backoff interval
            synchronized (retryLock) {
                try {
                    LOG.info("Reconnect backoff for {}ms", reconnectBackOff);
                    retryLock.wait(reconnectBackOff);
                } catch (InterruptedException e) {
                    LOG.error("Reconnect back off interrupted", e);
                }
            }
        }

        synchronized (startStopLock) {
            if (isStopping) {
                // flag off isStopping & within lock to
                return;
            }

            try {
                // create new connection to server - might fail
                connectSocket();

                // connect and authenticate - do this async
                // as TryReconnect is called from run loop
                keepAliveTimer.schedule(
                        this::connectAndAuthenticateAndResubscribe, 0, TimeUnit.MILLISECONDS);
            } catch (Exception e) {
                LOG.error("Reconnect attempt={} failed: ", reconnectCounter, e);
                reconnectCounter++;
            }
        }
    }

    private void connectAndAuthenticateAndResubscribe() {
        try {

            // Connect and auth
            connectAndAuthenticate();

            // Resub markets
            MarketSubscriptionMessage marketSubscription = processor.getMarketResubscribeMessage();
            if (marketSubscription != null) {
                LOG.info("Resubscribe to market subscription.");
                marketSubscription(marketSubscription);
            }

            // Resub orders
            OrderSubscriptionMessage orderSubscription = processor.getOrderResubscribeMessage();
            if (orderSubscription != null) {
                LOG.info("Resubscribe to order subscription.");
                orderSubscription(orderSubscription);
            }

            // Reset counter
            reconnectCounter = 0;
        } catch (Exception e) {
            LOG.error("Reconnect failed", e);
            reconnectCounter++;
        }
    }

    private void sendLineImpl(String line) throws ConnectionException {
        try {
            writer.write(line);
            writer.write(CRLF);
            writer.flush();
        } catch (IOException e) {
            LOG.error("Error sending to socket - disconnecting", e);
            // Foceably break the socket which will then trigger a reconnect if configured
            disconnect();
            throw new ConnectionException("Error sending to socket", e);
        }
    }

    /**
     * Subscribe the the specified orders (syncronously) with the configured HeartbeatMS,
     * ConflationMS.
     *
     * @param message
     * @throws IOException
     * @throws TimeoutException
     * @throws StatusException
     */
    public void orderSubscription(OrderSubscriptionMessage message)
            throws ConnectionException, StatusException {
        message.setConflateMs(conflateMs);
        message.setHeartbeatMs(heartbeatMs);
        waitFor(processor.orderSubscription(message));
    }

    /**
     * Subscribe the the specified markets (syncronously) with the configured HeartbeatMS,
     * ConflationMS.
     *
     * @param message
     * @throws IOException
     * @throws TimeoutException
     * @throws StatusException
     */
    public void marketSubscription(MarketSubscriptionMessage message)
            throws ConnectionException, StatusException {
        message.setConflateMs(conflateMs);
        message.setHeartbeatMs(heartbeatMs);
        message.setMarketDataFilter(marketDataFilter);
        waitFor(processor.marketSubscription(message));
    }

    public void heartbeat() throws ConnectionException, StatusException {
        waitFor(processor.heartbeat(new HeartbeatMessage()));
    }

    public boolean isAutoReconnect() {
        return autoReconnect;
    }

    public void setAutoReconnect(boolean autoReconnect) {
        this.autoReconnect = autoReconnect;
    }

    public long getTimeout() {
        return timeout;
    }

    public void setTimeout(long timeout) {
        this.timeout = timeout;
    }

    public long getReconnectBackOff() {
        return reconnectBackOff;
    }

    public void setReconnectBackOff(long reconnectBackOff) {
        this.reconnectBackOff = reconnectBackOff;
    }

    public long getKeepAliveHeartbeat() {
        return keepAliveHeartbeat;
    }

    public void setKeepAliveHeartbeat(long keepAliveHeartbeat) {
        this.keepAliveHeartbeat = keepAliveHeartbeat;
    }

    public Long getConflateMs() {
        return conflateMs;
    }

    public void setConflateMs(Long conflateMs) {
        this.conflateMs = conflateMs;
    }

    public Long getHeartbeatMs() {
        return heartbeatMs;
    }

    public void setHeartbeatMs(Long heartbeatMs) {
        this.heartbeatMs = heartbeatMs;
    }

    public MarketDataFilter getMarketDataFilter() {
        return marketDataFilter;
    }

    public void setMarketDataFilter(MarketDataFilter marketDataFilter) {
        this.marketDataFilter = marketDataFilter;
    }

    public void addConnectionStatusListener(ConnectionStatusListener listener) {
        processor.addConnectionStatusListener(listener);
    }

    public void removeConnectionStatusListener(ConnectionStatusListener listener) {
        processor.removeConnectionStatusListener(listener);
    }

    public ConnectionStatus getStatus() {
        return processor.getStatus();
    }

    public int getDisconnectCounter() {
        return disconnectCounter;
    }

    public int getReconnectCounter() {
        return reconnectCounter;
    }

    public int getTraceChangeTruncation() {
        return processor.getTraceChangeTruncation();
    }

    public void setTraceChangeTruncation(int traceChangeTruncation) {
        processor.setTraceChangeTruncation(traceChangeTruncation);
    }
}
