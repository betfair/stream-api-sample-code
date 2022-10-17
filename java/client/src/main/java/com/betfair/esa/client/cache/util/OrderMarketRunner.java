package com.betfair.esa.client.cache.util;

import com.betfair.esa.client.cache.order.OrderMarket;
import com.betfair.esa.swagger.model.Order;
import com.betfair.esa.swagger.model.OrderRunnerChange;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

public class OrderMarketRunner {

    private final OrderMarket orderMarket;
    private final RunnerId runnerId;

    private OrderMarketRunnerSnap orderMarketRunnerSnap;
    private final PriceSizeLadder layMatches = PriceSizeLadder.newLay();
    private final PriceSizeLadder backMatches = PriceSizeLadder.newBack();
    private final Map<String, Order> unmatchedOrders = new ConcurrentHashMap<>();

    public OrderMarketRunner(OrderMarket orderMarket, RunnerId runnerId) {
        this.orderMarket = orderMarket;
        this.runnerId = runnerId;
    }

    public void onOrderRunnerChange(OrderRunnerChange orderRunnerChange) {
        boolean isImage = Boolean.TRUE.equals(orderRunnerChange.isFullImage());

        if (isImage) {
            unmatchedOrders.clear();
        }

        if (orderRunnerChange.getUo() != null) {
            for (Order order : orderRunnerChange.getUo()) {
                unmatchedOrders.put(order.getId(), order);
            }
        }

        OrderMarketRunnerSnap newSnap = new OrderMarketRunnerSnap();
        newSnap.setUnmatchedOrders(new HashMap<>(unmatchedOrders));

        newSnap.setLayMatches(layMatches.onPriceChange(isImage, orderRunnerChange.getMl()));
        newSnap.setBackMatches(backMatches.onPriceChange(isImage, orderRunnerChange.getMb()));

        orderMarketRunnerSnap = newSnap;
    }

    public OrderMarketRunnerSnap getOrderMarketRunnerSnap() {
        return orderMarketRunnerSnap;
    }

    public RunnerId getRunnerId() {
        return runnerId;
    }

    public OrderMarket getOrderMarket() {
        return orderMarket;
    }

    @Override
    public String toString() {
        return orderMarketRunnerSnap == null ? "null" : orderMarketRunnerSnap.toString();
    }
}
