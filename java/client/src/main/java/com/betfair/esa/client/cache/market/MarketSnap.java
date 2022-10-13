package com.betfair.esa.client.cache.market;

import com.betfair.esa.swagger.model.MarketDefinition;
import java.util.List;

/**
 * Thread safe atomic snapshot of a market. Reference only changes if the snapshot changes: i.e. if
 * snap1 == snap2 then they are the same (same is true for sub-objects)
 */
public class MarketSnap {
    private String marketId;
    private MarketDefinition marketDefinition;
    private List<MarketRunnerSnap> marketRunners;
    private double tradedVolume;

    public String getMarketId() {
        return marketId;
    }

    void setMarketId(String marketId) {
        this.marketId = marketId;
    }

    public MarketDefinition getMarketDefinition() {
        return marketDefinition;
    }

    void setMarketDefinition(MarketDefinition marketDefinition) {
        this.marketDefinition = marketDefinition;
    }

    public List<MarketRunnerSnap> getMarketRunners() {
        return marketRunners;
    }

    void setMarketRunners(List<MarketRunnerSnap> marketRunners) {
        this.marketRunners = marketRunners;
    }

    public double getTradedVolume() {
        return tradedVolume;
    }

    void setTradedVolume(double tradedVolume) {
        this.tradedVolume = tradedVolume;
    }

    @Override
    public String toString() {
        return "MarketSnap{"
                + "marketId='"
                + marketId
                + '\''
                + ", marketDefinition="
                + marketDefinition
                + ", marketRunners="
                + marketRunners
                + ", tradedVolume="
                + tradedVolume
                + '}';
    }
}
