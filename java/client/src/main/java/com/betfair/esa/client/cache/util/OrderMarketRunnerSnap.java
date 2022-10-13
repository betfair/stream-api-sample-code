package com.betfair.esa.client.cache.util;

import com.betfair.esa.swagger.model.Order;
import java.util.List;
import java.util.Map;

public class OrderMarketRunnerSnap {

    private List<PriceSize> layMatches;
    private List<PriceSize> backMatches;

    private Map<String, Order> unmatchedOrders;

    public List<PriceSize> getLayMatches() {
        return layMatches;
    }

    public void setLayMatches(List<PriceSize> layMatches) {
        this.layMatches = layMatches;
    }

    public List<PriceSize> getBackMatches() {
        return backMatches;
    }

    public void setBackMatches(List<PriceSize> backMatches) {
        this.backMatches = backMatches;
    }

    public Map<String, Order> getUnmatchedOrders() {
        return unmatchedOrders;
    }

    public void setUnmatchedOrders(Map<String, Order> unmatchedOrders) {
        this.unmatchedOrders = unmatchedOrders;
    }

    @Override
    public String toString() {

        StringBuilder uoOrdersSb = new StringBuilder(" ");
        for (Order order : unmatchedOrders.values()) {
            uoOrdersSb.append(order).append(" ");
        }

        return "OrderMarketRunnerSnap{"
                + "layMatches="
                + layMatches
                + ", backMatches="
                + backMatches
                + ", unmatchedOrders="
                + uoOrdersSb
                + '}';
    }
}
