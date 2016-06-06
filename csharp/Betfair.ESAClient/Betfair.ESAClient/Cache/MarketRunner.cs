using Betfair.ESASwagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Cache
{
    /// <summary>
    /// Represents a market runner within a market
    /// </summary>
    public class MarketRunner
    {
        private readonly Market _market;
        private readonly RunnerId _runnerId;

        // Level / Depth Based Ladders
        private MarketRunnerPrices _runnerPrices = MarketRunnerPrices.EMPTY;
        private PriceSizeLadder _atlPrices = PriceSizeLadder.NewLay();
        private PriceSizeLadder _atbPrices = PriceSizeLadder.NewBack();
        private PriceSizeLadder _trdPrices = PriceSizeLadder.NewLay();
        private PriceSizeLadder _spbPrices = PriceSizeLadder.NewBack();
        private PriceSizeLadder _splPrices = PriceSizeLadder.NewLay();

        // Full depth Ladders
        private LevelPriceSizeLadder _batbPrices = new LevelPriceSizeLadder();
        private LevelPriceSizeLadder _batlPrices = new LevelPriceSizeLadder();
        private LevelPriceSizeLadder _bdatbPrices = new LevelPriceSizeLadder();
        private LevelPriceSizeLadder _bdatlPrices = new LevelPriceSizeLadder();

        // special prices
        private double _spn;
        private double _spf;
        private double _ltp;
        private double _tv;
        private RunnerDefinition _runnerDefinition;
        private MarketRunnerSnap _snap;



        public MarketRunner(Market market, RunnerId runnerId)
        {
            _market = market;
            _runnerId = runnerId;
        }

        internal void OnPriceChange(bool isImage, RunnerChange runnerChange)
        {
            //snap is invalid
            _snap = null;

            MarketRunnerPrices newPrices = new MarketRunnerPrices();


            newPrices.AvailableToLay = _atlPrices.OnPriceChange(isImage, runnerChange.Atl);
            newPrices.AvailableToBack = _atbPrices.OnPriceChange(isImage, runnerChange.Atb);
            newPrices.Traded = _trdPrices.OnPriceChange(isImage, runnerChange.Trd);
            newPrices.StartingPriceBack = _spbPrices.OnPriceChange(isImage, runnerChange.Spb);
            newPrices.StartingPriceLay = _splPrices.OnPriceChange(isImage, runnerChange.Spl);


            newPrices.BestAvailableToBack = _batbPrices.OnPriceChange(isImage, runnerChange.Batb);
            newPrices.BestAvailableToLay = _batlPrices.OnPriceChange(isImage, runnerChange.Batl);
            newPrices.BestDisplayAvailableToBack = _bdatbPrices.OnPriceChange(isImage, runnerChange.Bdatb);
            newPrices.BestDisplayAvailableToLay = _bdatlPrices.OnPriceChange(isImage, runnerChange.Bdatl);


            newPrices.StartingPriceNear = Utils.SelectPrice(isImage, ref _spn, runnerChange.Spn);
            newPrices.StartingPriceFar = Utils.SelectPrice(isImage, ref _spf, runnerChange.Spf);
            newPrices.LastTradedPrice = Utils.SelectPrice(isImage, ref _ltp, runnerChange.Ltp); 
            newPrices.TradedVolume = Utils.SelectPrice(isImage, ref _tv, runnerChange.Tv);

            //copy on write
            _runnerPrices = newPrices;
        }

        internal void OnRunnerDefinitionChange(RunnerDefinition runnerDefinition)
        {
            //snap is invalid
            _snap = null;

            _runnerDefinition = runnerDefinition;
        }

        public RunnerId RunnerId
        {
            get
            {
                return _runnerId;
            }
        }

        /// <summary>
        /// Takes or returns an existing immutable snap of the runner.
        /// </summary>
        public MarketRunnerSnap Snap
        {
            get
            {
                if(_snap == null)
                {
                    _snap = new MarketRunnerSnap()
                    {
                        RunnerId = RunnerId,
                        Definition = _runnerDefinition,
                        Prices = _runnerPrices
                    };
                }
                return _snap;
            }
        }


        public override string ToString()
        {
            return "MarketRunner{" +
                    "runnerId=" + _runnerId +
                    ", prices=" + _runnerPrices +
                    ", runnerDefinition=" + _runnerDefinition +
                    '}';
        }

    }
}
