package com.betfair.esa.client;

import com.betfair.esa.client.auth.AppKeyAndSessionProvider;
import com.betfair.esa.client.auth.InvalidCredentialException;
import com.betfair.esa.client.protocol.ConnectionException;
import com.betfair.esa.client.protocol.StatusException;
import org.testng.annotations.Test;

/** Created by mulveyj on 08/07/2016. */
public class ClientCacheTest extends BaseTest {

    @Test
    public void testUserStory()
            throws InvalidCredentialException, StatusException, ConnectionException,
                    InterruptedException {
        // 1: Create a session provider
        AppKeyAndSessionProvider sessionProvider =
                new AppKeyAndSessionProvider(
                        AppKeyAndSessionProvider.SSO_HOST_COM,
                        getAppKey(),
                        getUserName(),
                        getPassword());

        // 2: Create a client
        Client client = new Client("stream-api-integration.betfair.com", 443, sessionProvider);

        // 3: Create a cache
        ClientCache cache = new ClientCache(client);

        // 4: Setup order subscription
        // Register for change events
        //        cache.getOrderCache().OrderMarketChanged +=
        //                (sender, arg) => Console.WriteLine("Order:" + arg.Snap);
        // Subscribe to orders
        cache.subscribeOrders();

        // 5: Setup market subscription
        // Register for change events
        cache.getMarketCache()
                .addMarketChangeListener((e) -> System.out.println("Market:" + e.getSnap()));
        //        cache.MarketCache.MarketChanged +=
        //                (sender, arg) => Console.WriteLine("Market:" + arg.Snap);
        // Subscribe to markets (use a valid market id or filter)
        cache.subscribeMarkets("1.215134342");

        Thread.sleep(5000); // pause for a bit
    }
}
