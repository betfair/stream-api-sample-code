using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Cache
{
    public class OrderMarketSnap
    {
        public bool IsClosed { get; internal set; }
        public string MarketId { get; internal set; }
        public IEnumerable<OrderMarketRunnerSnap> OrderMarketRunners { get; internal set; }
    }
}
