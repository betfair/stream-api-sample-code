package com.betfair.esa.client;

import com.betfair.esa.client.auth.InvalidCredentialException;
import com.betfair.esa.client.protocol.ConnectionException;
import com.betfair.esa.client.protocol.ConnectionStatus;
import com.betfair.esa.client.protocol.StatusException;
import com.jayway.awaitility.Awaitility;
import com.jayway.awaitility.Duration;
import org.testng.Assert;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

/** Created by mulveyJ on 11/07/2016. */
public class ClientTest extends BaseTest {

    private Client client;

    @BeforeMethod
    public void beforeMethod() {
        client = new Client("stream-api-integration.betfair.com", 443, getValidSessionProvider());
    }

    @AfterMethod
    public void afterMethod() {
        client.stop();
    }

    @Test(expectedExceptions = ConnectionException.class)
    public void testInvalidHost()
            throws InvalidCredentialException, StatusException, ConnectionException {
        Client invalidClient = new Client("www.betfair.com", 443, getValidSessionProvider());
        invalidClient.setTimeout(100);
        invalidClient.start();
    }

    @Test
    public void testStartStop()
            throws InvalidCredentialException, StatusException, ConnectionException {
        Assert.assertEquals(client.getStatus(), ConnectionStatus.STOPPED);
        client.start();
        Assert.assertEquals(client.getStatus(), ConnectionStatus.AUTHENTICATED);
        client.stop();
        Assert.assertEquals(client.getStatus(), ConnectionStatus.STOPPED);
    }

    @Test
    public void testStartHeartbeatStop()
            throws InvalidCredentialException, StatusException, ConnectionException {
        client.start();
        client.heartbeat();
        client.stop();
    }

    @Test
    public void testReentrantStartStop()
            throws InvalidCredentialException, StatusException, ConnectionException {
        client.start();
        Assert.assertEquals(client.getStatus(), ConnectionStatus.AUTHENTICATED);
        client.heartbeat();
        client.stop();
        Assert.assertEquals(client.getStatus(), ConnectionStatus.STOPPED);

        client.start();
        Assert.assertEquals(client.getStatus(), ConnectionStatus.AUTHENTICATED);
        client.heartbeat();
        client.stop();
        Assert.assertEquals(client.getStatus(), ConnectionStatus.STOPPED);
    }

    @Test
    public void testDoubleStartStop()
            throws InvalidCredentialException, StatusException, ConnectionException {
        client.start();
        client.start();
        Assert.assertEquals(client.getStatus(), ConnectionStatus.AUTHENTICATED);
        client.heartbeat();
        client.stop();
        client.stop();
        Assert.assertEquals(client.getStatus(), ConnectionStatus.STOPPED);
    }

    @Test
    public void testDisconnectWithAutoReconnect()
            throws InvalidCredentialException, StatusException, ConnectionException {
        client.start();
        Assert.assertEquals(client.getStatus(), ConnectionStatus.AUTHENTICATED);
        client.heartbeat();

        // socket disconnect
        Assert.assertEquals(client.getDisconnectCounter(), 0);
        client.disconnect();

        // retry until connected
        Awaitility.await()
                .catchUncaughtExceptions()
                .atMost(Duration.ONE_MINUTE)
                .until(
                        () -> {
                            try {
                                client.heartbeat();
                                return true;
                            } catch (Throwable e) {
                                return false;
                            }
                        });
        Assert.assertEquals(client.getStatus(), ConnectionStatus.AUTHENTICATED);
        Assert.assertEquals(client.getDisconnectCounter(), 1);
    }
}
