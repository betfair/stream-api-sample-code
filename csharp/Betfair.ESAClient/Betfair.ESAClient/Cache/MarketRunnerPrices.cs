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
            AvailableToLay = PriceSize.EmptyList,
            AvailableToBack = PriceSize.EmptyList,
            Traded = PriceSize.EmptyList,
            StartingPriceBack = PriceSize.EmptyList,
            StartingPriceLay = PriceSize.EmptyList,

            BestAvailableToBack = LevelPriceSize.EmptyList,
            BestAvailableToLay = LevelPriceSize.EmptyList,
            BestDisplayAvailableToBack = LevelPriceSize.EmptyList,
            BestDisplayAvailableToLay = LevelPriceSize.EmptyList,
        };

        public IList<PriceSize> AvailableToLay { get; internal set; } 
        public IList<PriceSize> AvailableToBack { get; internal set; }
        public IList<PriceSize> Traded { get; internal set; }
        public IList<PriceSize> StartingPriceBack { get; internal set; }
        public IList<PriceSize> StartingPriceLay { get; internal set; }

        public IList<LevelPriceSize> BestAvailableToBack { get; internal set; }
        public IList<LevelPriceSize> BestAvailableToLay { get; internal set; }
        public IList<LevelPriceSize> BestDisplayAvailableToBack { get; internal set; }
        public IList<LevelPriceSize> BestDisplayAvailableToLay { get; internal set; }

        public double LastTradedPrice { get; internal set; }
        public double StartingPriceNear { get; internal set; }
        public double StartingPriceFar { get; internal set; }
        public double TradedVolume { get; internal set; }

        public override string ToString()
        {
            return "MarketRunnerPrices{" +
                "AvailableToLay=" + String.Join(", ", AvailableToLay) +
                ", AvailableToBack=" + String.Join(", ", AvailableToBack) +
                ", Traded=" + String.Join(", ", Traded) +
                ", StartingPriceBack=" + String.Join(", ", StartingPriceBack) +
                ", StartingPriceLay=" + String.Join(", ", StartingPriceLay) +

                ", BestAvailableToBack=" + String.Join(", ", BestAvailableToBack) +
                ", BestAvailableToLay=" + String.Join(", ", BestAvailableToLay) +
                ", BestDisplayAvailableToBack=" + String.Join(", ", BestDisplayAvailableToBack) +
                ", BestDisplayAvailableToLay=" + String.Join(", ", BestDisplayAvailableToLay) +

                ", LastTradedPrice=" + LastTradedPrice +
                ", StartingPriceNear=" + StartingPriceNear +
                ", StartingPriceFar=" + StartingPriceFar +
                ", TradedVolume=" + TradedVolume +
                "}";
        }
    }
}
