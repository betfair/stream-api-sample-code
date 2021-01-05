using System;
using System.Threading;
using Betfair.ESAClient.Auth;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Betfair.ESAClient.Test {
    [TestClass]
    public class ClientCacheTest : BaseTest {
        [TestMethod]
        public void TestUserStory() {
            //1: Create a session provider
            var sessionProvider = new AppKeyAndSessionProvider(AppKeyAndSessionProvider.SSO_HOST_COM,
                AppKey,
                UserName,
                Password);

            //2: Create a client
            var client = new Client("stream-api-integration.betfair.com", 443, sessionProvider);

            //3: Create a cache
            var cache = new ClientCache(client);

            //4: Setup order subscription
            //Register for change events
            cache.OrderCache.OrderMarketChanged += (sender, arg) => Console.WriteLine("Order:" + arg.Snap);
            //Subscribe to orders    
            cache.SubscribeOrders();

            //5: Setup market subscription
            //Register for change events
            cache.MarketCache.MarketChanged += (sender, arg) => Console.WriteLine("Market:" + arg.Snap);
            //Subscribe to markets (use a valid market id or filter)
            cache.SubscribeMarkets("1.125037533");

            Thread.Sleep(5000); //pause for a bit
        }
    }
}
