using Betfair.ESASwagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Cache
{
    /// <summary>
    /// Thread safe atomic snapshot of a market runner.
    /// Reference only changes if the snapshot changes:
    /// i.e. if snap1 == snap2 then they are the same.
    /// (same is true for sub-objects)
    /// </summary>
    public class MarketRunnerSnap
    {
        public RunnerId RunnerId { get; internal set; }
        public RunnerDefinition Definition { get; internal set; }
        public MarketRunnerPrices Prices { get; internal set; }

        public override string ToString()
        {
            return "MarketRunnerSnap{" +
                    "runnerId=" + RunnerId +
                    ", prices=" + Prices +
                    ", definition=" + Definition +
                    '}';
        }
    }
}
