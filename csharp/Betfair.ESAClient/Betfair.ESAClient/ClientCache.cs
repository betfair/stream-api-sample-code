using Betfair.ESAClient.Cache;
using Betfair.ESAClient.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Betfair.ESASwagger.Model;

namespace Betfair.ESAClient
{
    /// <summary>
    /// Simple ESA Cache implementation that wraps an ESA Client
    /// and caches the streams of data.
    /// </summary>
    public class ClientCache : IChangeMessageHandler
    {
        private readonly MarketCache _marketCache = new MarketCache();
        private readonly OrderCache _orderCache = new OrderCache();
        private readonly Client _client;

        /// <summary>
        /// Construct a new cache to consume from / wrap the specified client.
        /// </summary>
        /// <param name="client"></param>
        public ClientCache(Client client)
        {
            _client = client;
            _client.ChangeHandler = this;
        }

        /// <summary>
        /// The underlying Client
        /// </summary>
        public Client Client
        {
            get
            {
                return _client;
            }
        }

        /// <summary>
        /// The underlying Client status
        /// </summary>
        public ConnectionStatus Status
        {
            get
            {
                return _client.Status;
            }
        }
            
        /// <summary>
        /// Explicitly start the client (otherwise this is automatic on first subscribe).
        /// </summary>
        public void Start()
        {
            _client.Start();
        }

        /// <summary>
        /// Explicitly stop the client
        /// </summary>
        public void Stop()
        {
            _client.Stop();
        }

        /// <summary>
        /// Set a common market data filter for all market subscriptions
        /// </summary>
        public MarketDataFilter MarketDataFilter
        {
            get
            {
                return _client.MarketDataFilter;
            }
            set
            {
                _client.MarketDataFilter = value;
            }
        }

        /// <summary>
        /// Set a common requested conflation rate (this slows rate of changes down) in milliseconds.
        /// </summary>
        public long? ConflatMs
        {
            get
            {
                return _client.ConflateMs;
            }
            set
            {
                _client.ConflateMs = value;
            }
        }

        /// <summary>
        /// Subscribe to all orders. (starting the client if needed).
        /// </summary>
        public void SubscribeOrders()
        {
            SubscribeOrders(new OrderSubscriptionMessage());
        }

        /// <summary>
        /// Explict order subscription. (starting the client if needed).
        /// </summary>
        /// <param name="subscription"></param>
        public void SubscribeOrders(OrderSubscriptionMessage subscription)
        {
            _client.Start();
            _client.OrderSubscription(subscription);
        }

        /// <summary>
        /// Subscribe to the specified market ids. (starting the client if needed).
        /// </summary>
        /// <param name="markets"></param>
        public void SubscribeMarkets(params string[] markets)
        {
            SubscribeMarkets(new MarketFilter { MarketIds = new List<string>(markets) });
        }

        /// <summary>
        /// Subscribe to the specified markets (matching your filter). (starting the client if needed).
        /// </summary>
        /// <param name="marketFilter"></param>
        public void SubscribeMarkets(MarketFilter marketFilter)
        {
            SubscribeMarkets(new MarketSubscriptionMessage()
            {
                MarketFilter = marketFilter,
            });
        }

        /// <summary>
        /// Explicit order subscripion. (starting the client if needed).
        /// </summary>
        /// <param name="subscription"></param>
        public void SubscribeMarkets(MarketSubscriptionMessage subscription)
        {
            _client.Start();
            _client.MarketSubscription(subscription);
        }

        /// <summary>
        /// The cache of all subscribed markets
        /// </summary>
        public MarketCache MarketCache
        {
            get
            {
                return _marketCache;
            }
        }

        /// <summary>
        /// The cache of all subscribed orders
        /// </summary>
        public OrderCache OrderCache
        {
            get
            {
                return _orderCache;
            }
        }


        #region IChangeMessageHandler

        void IChangeMessageHandler.OnMarketChange(ChangeMessage<MarketChange> change)
        {
            _marketCache.OnMarketChange(change);
        }

        void IChangeMessageHandler.OnOrderChange(ChangeMessage<OrderMarketChange> change)
        {
            _orderCache.OnOrderChange(change);
        }

        void IChangeMessageHandler.OnErrorStatusNotification(StatusMessage message)
        {

        }

        #endregion

    }
}
