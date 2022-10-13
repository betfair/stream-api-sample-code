package com.betfair.esa.client.cache.market;

import com.betfair.esa.client.cache.util.RunnerId;
import com.betfair.esa.client.cache.util.Utils;
import com.betfair.esa.swagger.model.MarketChange;
import com.betfair.esa.swagger.model.MarketDefinition;
import com.betfair.esa.swagger.model.RunnerChange;
import com.betfair.esa.swagger.model.RunnerDefinition;
import java.util.Map;
import java.util.Optional;
import java.util.concurrent.ConcurrentHashMap;
import java.util.stream.Collectors;

/**
 * Thread safe, reference invariant reference to a market. Repeatedly calling <see cref="Snap"/>
 * will return atomic snapshots of the market.
 */
public class Market {
    private final MarketCache marketCache;
    private final String marketId;
    private final Map<RunnerId, MarketRunner> marketRunners = new ConcurrentHashMap<>();
    private MarketDefinition marketDefinition;
    private double tv;
    // An atomic snapshot of the state of the market.
    private MarketSnap snap;

    public Market(MarketCache clientMarketCache, String marketId) {
        this.marketCache = clientMarketCache;
        this.marketId = marketId;
    }

    void onMarketChange(MarketChange marketChange) {
        // initial image means we need to wipe our data
        boolean isImage = Boolean.TRUE.equals(marketChange.isImg());
        // market definition changed
        Optional.ofNullable(marketChange.getMarketDefinition())
                .ifPresent(this::onMarketDefinitionChange);
        // runners changed
        Optional.ofNullable(marketChange.getRc())
                .ifPresent(l -> l.forEach(p -> onPriceChange(isImage, p)));

        MarketSnap newSnap = new MarketSnap();
        newSnap.setMarketId(marketId);
        newSnap.setMarketDefinition(marketDefinition);
        newSnap.setMarketRunners(
                marketRunners.values().stream()
                        .map(MarketRunner::getSnap)
                        .collect(Collectors.toList()));
        newSnap.setTradedVolume(tv = Utils.selectPrice(isImage, tv, marketChange.getTv()));
        snap = newSnap;
    }

    private void onPriceChange(boolean isImage, RunnerChange runnerChange) {
        MarketRunner marketRunner =
                getOrAdd(new RunnerId(runnerChange.getId(), runnerChange.getHc()));
        // update runner
        marketRunner.onPriceChange(isImage, runnerChange);
    }

    private void onMarketDefinitionChange(MarketDefinition marketDefinition) {
        this.marketDefinition = marketDefinition;
        Optional.ofNullable(marketDefinition.getRunners())
                .ifPresent(rds -> rds.forEach(this::onRunnerDefinitionChange));
    }

    private void onRunnerDefinitionChange(RunnerDefinition runnerDefinition) {
        MarketRunner marketRunner =
                getOrAdd(new RunnerId(runnerDefinition.getId(), runnerDefinition.getHc()));
        // update runner
        marketRunner.onRunnerDefinitionChange(runnerDefinition);
    }

    private MarketRunner getOrAdd(RunnerId runnerId) {
        MarketRunner runner =
                marketRunners.computeIfAbsent(runnerId, k -> new MarketRunner(this, k));
        return runner;
    }

    public String getMarketId() {
        return marketId;
    }

    public boolean isClosed() {
        // whether the market is closed
        return (marketDefinition != null
                && marketDefinition.getStatus() == MarketDefinition.StatusEnum.CLOSED);
    }

    public MarketSnap getSnap() {
        return snap;
    }

    @Override
    public String toString() {
        return "Market{"
                + "marketId='"
                + marketId
                + '\''
                + ", marketRunners="
                + marketRunners
                + ", marketDefinition="
                + marketDefinition
                + '}';
    }
}
