# stream-api-sample-code
Sample code for the exchange stream api which provides real-time market and order data from the betfair exchange.

# Console App
The console app enables you to explore most of the API functions; please note that:

1. Your appkey must be setup for streaming (contact BDP)
2. Credentials for .net are stored in AppData in plain text

# Client overview
The basic client structure is separated into a number of components:
* AppKeyAndSessionProvider - this class is used to authenticate & provide a session token.
* Client - this class provides a connection to the stream
* ClientCache - this class provides a thread safe cache that may be used to:
 * Respond to discrete changes
 * Respond to batch changes
 * Directly query the cache

# Getting started
The below is found in the ClientCacheTest and exhibits the basic setup user story:

            //1: Create a session provider
            AppKeyAndSessionProvider sessionProvider = new AppKeyAndSessionProvider(
                AppKeyAndSessionProvider.SSO_HOST_COM,
                AppKey,
                UserName,
                Password);

            //2: Create a client
            Client client = new Client(
                "stream-api-integration.betfair.com",
                443,
                sessionProvider);

            //3: Create a cache
            ClientCache cache = new ClientCache(client);

            //4: Setup order subscription
            //Register for change events
            cache.OrderCache.OrderMarketChanged += 
                (sender, arg) => Console.WriteLine("Order:" + arg.Snap);
            //Subscribe to orders    
            cache.SubscribeOrders();

            //5: Setup market subscription
            //Register for change events
            cache.MarketCache.MarketChanged += 
                (sender, arg) => Console.WriteLine("Market:" + arg.Snap);
            //Subscribe to markets (use a valid market id or filter)
            cache.SubscribeMarkets("1.125037533");

## Connection semantics
A few tips on basic connections:

1. No need to explicitly start / stop if using client cache
2. Status event allows you to monitor status
3. AutoReconnect is enabled by default and will establish and re-subscribe any subscriptions.
4. Exceptions are routed up to subscribe methods

## Market Subscription
A few tips on market subscription:

1. MarketDataFilter - correctly setting this on the cache improves performance
2. MarketFilter - you are not limited to knowing specific market ids; wildcards let you know as soon as a market appears
3. ConflateMs - you can slow down the data rate (to say a GUI refresh rate).
4. By default markets are deleted on close

## Order Subscription
A few tips on order subscription:

1. Orders are retrieved for your id
2. On initial connection (or re-connect) only unmatched / executable orders are returned.
3. Matches are price point aggregated
4. By default markets are deleted on close

