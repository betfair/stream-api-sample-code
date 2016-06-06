using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Betfair.ESASwagger.Model;

namespace Betfair.ESAClient.Cache
{
    /// <summary>
    /// Thread safe atomic snapshot of a market.
    /// Reference only changes if the snapshot changes:
    /// i.e. if snap1 == snap2 then they are the same.
    /// (same is true for sub-objects)
    /// </summary>
    public class MarketSnap
    {
        public MarketDefinition MarketDefinition { get; internal set; }
        public string MarketId { get; internal set; }
        public IList<MarketRunnerSnap> MarketRunners { get; internal set; }
        public double TradedVolume { get; internal set; }

        public override string ToString()
        {
            return "MarketSnap{" +
                "MarketId=" + MarketId +
                ", MarketDefinition=" + MarketDefinition +
                ", MarketRunners=" + String.Join(", ", MarketRunners) +
                ", TradedVolume=" + TradedVolume +
                "}";
        }
    }
}
