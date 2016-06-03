using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Betfair.ESASwagger.Model;

namespace Betfair.ESAClient.Cache
{
    public class OrderMarketRunnerSnap
    {
        public IList<PriceSize> MatchedLay { get; internal set; }
        public IList<PriceSize> MatchedBack { get; internal set; }
        public Dictionary<string, Order> UnmatchedOrders { get; internal set; }

        public override string ToString()
        {
            return "OrderMarketRunnerSnap{" +
                "UnmatchedOrders=" + String.Join(", ", UnmatchedOrders.Values) +
                ", MatchedLay=" + String.Join(", ", MatchedLay) +
                ", MatchedBack=" + String.Join(", ", MatchedBack) +
                "}";
        }
    }
}
