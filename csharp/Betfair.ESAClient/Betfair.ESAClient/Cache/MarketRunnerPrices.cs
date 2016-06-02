using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Cache
{
    /// <summary>
    /// Atomic snap of the prices associated with a runner.
    /// </summary>
    public class MarketRunnerPrices
    {
        public static readonly MarketRunnerPrices EMPTY = new MarketRunnerPrices()
        {
            Atl = PriceSize.EmptyList,
            Atb = PriceSize.EmptyList,
            Trd = PriceSize.EmptyList,
            Spb = PriceSize.EmptyList,
            Spl = PriceSize.EmptyList,

            Batb = LevelPriceSize.EmptyList,
            Batl = LevelPriceSize.EmptyList,
            Bdatb = LevelPriceSize.EmptyList,
            Bdatl = LevelPriceSize.EmptyList,
        };

        public IList<PriceSize> Atl { get; internal set; } 
        public IList<PriceSize> Atb { get; internal set; }
        public IList<PriceSize> Trd { get; internal set; }
        public IList<PriceSize> Spb { get; internal set; }
        public IList<PriceSize> Spl { get; internal set; }

        public IList<LevelPriceSize> Batb { get; internal set; }
        public IList<LevelPriceSize> Batl { get; internal set; }
        public IList<LevelPriceSize> Bdatb { get; internal set; }
        public IList<LevelPriceSize> Bdatl { get; internal set; }

        public double Ltp { get; internal set; }
        public double Spn { get; internal set; }
        public double Spf { get; internal set; }
        public double Tv { get; internal set; }

        public override string ToString()
        {
            return "MarketRunnerPrices{" +
                "Atl=" + String.Join(", ", Atl) +
                ", Atb=" + String.Join(", ", Atb) +
                ", Trd=" + String.Join(", ", Trd) +
                ", Spb=" + String.Join(", ", Spb) +
                ", Spl=" + String.Join(", ", Spl) +

                ", Batb=" + String.Join(", ", Batb) +
                ", Batl=" + String.Join(", ", Batl) +
                ", Bdatb=" + String.Join(", ", Bdatb) +
                ", Bdatl=" + String.Join(", ", Bdatl) +

                ", Ltp=" + Ltp +
                ", Spn=" + Spn +
                ", Spf=" + Spf +
                ", Tv=" + Tv +
                "}";
        }
    }
}
