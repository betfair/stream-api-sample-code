# stream-api-sample-code
Sample code for the exchange stream api which provides real-time market and order data from the betfair exchange.

# Console App
The console app enables you to explore most of the API functions; please note that:


1. Your appkey must be setup for streaming (contact Developer Support  [here](https://betfair-developer-docs.atlassian.net/wiki/spaces/1smk3cen4v3lu3yomq5qye0ni/pages/2687786/Getting+Started)  )

2. Credentials for .net are stored in AppData in plain text

**Note: Use stream-api.betfair.com in production (stream-api-integration.betfair.com is for testing and has no backup).**

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
                "stream-api-integration.betfair.com", //NOTE: use production endpoint in prod: stream-api.betfair.com
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

## Docker
It is possible to run the java application through a docker container, if you want to build yourself and run, execute on parent directory:
1. docker build -t "tag" -f java/Dockerfile .
2. docker run -it "tag"

Otherwise make use of docker-compose features:
1. Simply with a single command "docker-compose run esaconsole" will set everything up.

## Specification & Schema
###Specification: 
https://github.com/betfair/stream-api-sample-code/blob/master/stream-api-specification.pdf

###Schema
We publish a swagger schema to define the object model:
http://editor.swagger.io/#/?import=https://raw.githubusercontent.com/betfair/stream-api-sample-code/master/ESASwaggerSchema.json
(_Note: the stream is not a rest service so generated swagger clients are of limited use although you can use the generated object model_)
