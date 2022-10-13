package com.betfair.esa.client.cache.market;

import com.betfair.esa.client.cache.util.RunnerId;
import com.betfair.esa.swagger.model.RunnerDefinition;

/**
 * Thread safe atomic snapshot of a market runner. Reference only changes if the snapshot changes:
 * i.e. if snap1 == snap2 then they are the same (same is true for sub-objects)
 */
public class MarketRunnerSnap {
    private RunnerId runnerId;
    private RunnerDefinition definition;
    private MarketRunnerPrices prices;

    public MarketRunnerSnap(
            RunnerId runnerId, RunnerDefinition definition, MarketRunnerPrices prices) {
        this.runnerId = runnerId;
        this.definition = definition;
        this.prices = prices;
    }

    public RunnerId getRunnerId() {
        return runnerId;
    }

    void setRunnerId(RunnerId runnerId) {
        this.runnerId = runnerId;
    }

    public RunnerDefinition getDefinition() {
        return definition;
    }

    void setDefinition(RunnerDefinition definition) {
        this.definition = definition;
    }

    public MarketRunnerPrices getPrices() {
        return prices;
    }

    void setPrices(MarketRunnerPrices prices) {
        this.prices = prices;
    }

    @Override
    public String toString() {
        return "MarketRunnerSnap{"
                + "runnerId="
                + runnerId
                + ", definition="
                + definition
                + ", prices="
                + prices
                + '}';
    }
}
