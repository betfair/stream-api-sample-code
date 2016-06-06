using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Betfair.ESASwagger.Model;

namespace Betfair.ESAClient.Cache
{
    /// <summary>
    /// The cached state of the market
    /// </summary>
    public class OrderMarket
    {
        private readonly OrderCache _orderCache;
        private readonly string _marketId;
        private readonly Dictionary<RunnerId, OrderMarketRunner> _marketRunners = new Dictionary<RunnerId, OrderMarketRunner>();
        private OrderMarketSnap _snap;

        public OrderMarket(OrderCache orderCache, string marketId)
        {
            _orderCache = orderCache;
            _marketId = marketId;
        }

        internal void OnOrderMarketChange(OrderMarketChange orderMarketChange)
        {            

            OrderMarketSnap newSnap = new OrderMarketSnap();
            newSnap.MarketId = _marketId;

            //update runners
            if (orderMarketChange.Orc != null)
            {
                //runners changed
                foreach (OrderRunnerChange orderRunnerChange in orderMarketChange.Orc)
                {
                    OnOrderRunnerChange(orderRunnerChange);
                }
            }
            newSnap.OrderMarketRunners = _marketRunners.Values.Select(omr => omr.Snap);

            //update closed
            IsClosed = orderMarketChange.Closed == true;
            newSnap.IsClosed = IsClosed;

            _snap = newSnap;
        }

        private void OnOrderRunnerChange(OrderRunnerChange orderRunnerChange)
        {
            RunnerId rid = new RunnerId(orderRunnerChange.Id, orderRunnerChange.Hc);
            OrderMarketRunner orderMarketRunner;
            if (!_marketRunners.TryGetValue(rid, out orderMarketRunner))
            {
                orderMarketRunner = new OrderMarketRunner(this, rid);
                _marketRunners[rid] = orderMarketRunner;
            }
            //update the runner
            orderMarketRunner.OnOrderRunnerChange(orderRunnerChange);
        }

        public string MarketId
        {
            get
            {
                return _marketId;
            }
        }

        public bool IsClosed { get; private set; }

        /// <summary>
        /// Takes or returns an existing immutable snap of the market.
        /// </summary>
        public OrderMarketSnap Snap
        {
            get
            {
                return _snap;
            }
        }

        public override string ToString()
        {
            return "OrderMarket{" +
                "MarketId=" + MarketId +
                ", Runners=" + String.Join(", ", _marketRunners.Values) +
                "}";
        }

    }
}
