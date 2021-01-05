using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Cache {
    /// <summary>
    /// Thread safe atomic snapshot of a market.
    /// Reference only changes if the snapshot changes:
    /// i.e. if snap1 == snap2 then they are the same.
    /// (same is true for sub-objects)
    /// </summary>
    public class OrderMarketSnap {
        public string MarketId { get; internal set; }
        public bool IsClosed { get; internal set; }
        public IEnumerable<OrderMarketRunnerSnap> OrderMarketRunners { get; internal set; }

        public override string ToString() {
            return "OrderMarketSnap{" + "MarketId=" + MarketId + ", IsClosed=" + IsClosed + ", OrderMarketRunners=" + String.Join(", ", OrderMarketRunners) + "}";
        }
    }
}
