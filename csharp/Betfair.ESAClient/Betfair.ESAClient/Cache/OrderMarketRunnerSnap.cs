using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Betfair.ESASwagger.Model;

namespace Betfair.ESAClient.Cache
{

    /// <summary>
    /// Thread safe atomic snapshot of a runner.
    /// Reference only changes if the snapshot changes:
    /// i.e. if snap1 == snap2 then they are the same.
    /// (same is true for sub-objects)
    /// </summary>
    public class OrderMarketRunnerSnap
    {
        /// <summary>
        /// Runner id.
        /// </summary>
        public RunnerId RunnerId { get; internal set; }
        /// <summary>
        /// Price point aggregations of matches
        /// </summary>
        public IList<PriceSize> MatchedLay { get; internal set; }
        /// <summary>
        /// Price point aggregations of matches
        /// </summary>
        public IList<PriceSize> MatchedBack { get; internal set; }
        /// <summary>
        /// Orders that are unmatched (this includes the transiont to Execution Complete; but this
        /// is not the case on an initial image).
        /// </summary>
        public Dictionary<string, Order> UnmatchedOrders { get; internal set; }


        public override string ToString()
        {
            return "OrderMarketRunnerSnap{" +
                "RunnerId=" + RunnerId + 
                ", UnmatchedOrders=" + String.Join(", ", UnmatchedOrders.Values) +
                ", MatchedLay=" + String.Join(", ", MatchedLay) +
                ", MatchedBack=" + String.Join(", ", MatchedBack) +
                "}";
        }
    }
}
