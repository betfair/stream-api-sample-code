package com.betfair.esa.client.cache.market;

import com.betfair.esa.client.cache.util.LevelPriceSizeLadder;
import com.betfair.esa.client.cache.util.PriceSizeLadder;
import com.betfair.esa.client.cache.util.RunnerId;
import com.betfair.esa.client.cache.util.Utils;
import com.betfair.esa.swagger.model.RunnerChange;
import com.betfair.esa.swagger.model.RunnerDefinition;

public class MarketRunner {
    private final Market market;
    private final RunnerId runnerId;

    // Level / Depth Based Ladders
    private MarketRunnerPrices marketRunnerPrices = new MarketRunnerPrices();
    private final PriceSizeLadder atlPrices = PriceSizeLadder.newLay();
    private final PriceSizeLadder atbPrices = PriceSizeLadder.newBack();
    private final PriceSizeLadder trdPrices = PriceSizeLadder.newLay();
    private final PriceSizeLadder spbPrices = PriceSizeLadder.newBack();
    private final PriceSizeLadder splPrices = PriceSizeLadder.newLay();

    // Full depth Ladders
    private final LevelPriceSizeLadder batbPrices = new LevelPriceSizeLadder();
    private final LevelPriceSizeLadder batlPrices = new LevelPriceSizeLadder();
    private final LevelPriceSizeLadder bdatbPrices = new LevelPriceSizeLadder();
    private final LevelPriceSizeLadder bdatlPrices = new LevelPriceSizeLadder();

    // special prices
    private double spn;
    private double spf;
    private double ltp;
    private double tv;
    private RunnerDefinition runnerDefinition;
    private MarketRunnerSnap snap;

    public MarketRunner(Market market, RunnerId runnerId) {
        this.market = market;
        this.runnerId = runnerId;
    }

    void onPriceChange(boolean isImage, RunnerChange runnerChange) {
        // snap is invalid
        snap = null;

        MarketRunnerPrices newPrices = new MarketRunnerPrices();

        newPrices.atl = atlPrices.onPriceChange(isImage, runnerChange.getAtl());
        newPrices.atb = atbPrices.onPriceChange(isImage, runnerChange.getAtb());
        newPrices.trd = trdPrices.onPriceChange(isImage, runnerChange.getTrd());
        newPrices.spb = spbPrices.onPriceChange(isImage, runnerChange.getSpb());
        newPrices.spl = splPrices.onPriceChange(isImage, runnerChange.getSpl());

        newPrices.batb = batbPrices.onPriceChange(isImage, runnerChange.getBatb());
        newPrices.batl = batlPrices.onPriceChange(isImage, runnerChange.getBatl());
        newPrices.bdatb = bdatbPrices.onPriceChange(isImage, runnerChange.getBdatb());
        newPrices.bdatl = bdatlPrices.onPriceChange(isImage, runnerChange.getBdatl());

        newPrices.spn = spn = Utils.selectPrice(isImage, spn, runnerChange.getSpn());
        newPrices.spf = spf = Utils.selectPrice(isImage, spf, runnerChange.getSpf());
        newPrices.ltp = ltp = Utils.selectPrice(isImage, ltp, runnerChange.getLtp());
        newPrices.tv = tv = Utils.selectPrice(isImage, tv, runnerChange.getTv());

        // copy on write
        marketRunnerPrices = newPrices;
    }

    void onRunnerDefinitionChange(RunnerDefinition runnerDefinition) {
        // snap is invalid
        snap = null;
        this.runnerDefinition = runnerDefinition;
    }

    public RunnerId getRunnerId() {
        return runnerId;
    }

    public MarketRunnerSnap getSnap() {
        // takes or returns an existing immutable snap of the runner
        if (snap == null) {
            snap = new MarketRunnerSnap(getRunnerId(), runnerDefinition, marketRunnerPrices);
        }
        return snap;
    }

    @Override
    public String toString() {
        return "MarketRunner{"
                + "runnerId="
                + runnerId
                + ", prices="
                + marketRunnerPrices
                + ", runnerDefinition="
                + runnerDefinition
                + '}';
    }
}
