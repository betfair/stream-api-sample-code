using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Cache
{
    public class OrderMarketSnap
    {
        public string MarketId { get; internal set; }
        public bool IsClosed { get; internal set; }
        public IEnumerable<OrderMarketRunnerSnap> OrderMarketRunners { get; internal set; }

        public override string ToString()
        {
            return "OrderMarketSnap{" +
                "MarketId=" + MarketId+
                ", IsClosed=" + IsClosed +
                ", OrderMarketRunners=" + String.Join(", ", OrderMarketRunners) +
                "}";
        }
    }
}
