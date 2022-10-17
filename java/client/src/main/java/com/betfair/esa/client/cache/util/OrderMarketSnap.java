package com.betfair.esa.client.cache.util;

public class OrderMarketSnap {

    private String marketId;
    private boolean isClosed;
    private Iterable<OrderMarketRunnerSnap> orderMarketRunners;

    public boolean isClosed() {
        return isClosed;
    }

    public void setClosed(boolean closed) {
        isClosed = closed;
    }

    public Iterable<OrderMarketRunnerSnap> getOrderMarketRunners() {
        return orderMarketRunners;
    }

    public void setOrderMarketRunners(Iterable<OrderMarketRunnerSnap> orderMarketRunners) {
        this.orderMarketRunners = orderMarketRunners;
    }

    public String getMarketId() {
        return marketId;
    }

    public void setMarketId(String marketId) {
        this.marketId = marketId;
    }

    @Override
    public String toString() {
        return "OrderMarketSnap{"
                + "marketId='"
                + marketId
                + '\''
                + ", isClosed="
                + isClosed
                + ", orderMarketRunners="
                + orderMarketRunners
                + '}';
    }
}
