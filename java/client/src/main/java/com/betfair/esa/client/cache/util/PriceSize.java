package com.betfair.esa.client.cache.util;

import java.util.List;

public class PriceSize {
    private final double price;
    private final double size;

    public PriceSize(List<Double> priceSize) {
        this.price = priceSize.get(0);
        this.size = priceSize.get(1);
    }

    public double getPrice() {
        return price;
    }

    public double getSize() {
        return size;
    }

    @Override
    public String toString() {
        return size + "@" + price;
    }
}
