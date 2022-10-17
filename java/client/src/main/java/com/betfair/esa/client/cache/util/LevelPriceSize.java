package com.betfair.esa.client.cache.util;

import java.util.List;

public class LevelPriceSize {
    private final int level;
    private final double price;
    private final double size;

    public LevelPriceSize(List<Double> levelPriceSize) {
        level = levelPriceSize.get(0).intValue();
        price = levelPriceSize.get(1);
        size = levelPriceSize.get(2);
    }

    public LevelPriceSize(int level, double price, double size) {
        this.level = level;
        this.price = price;
        this.size = size;
    }

    public int getLevel() {
        return level;
    }

    public double getPrice() {
        return price;
    }

    public double getSize() {
        return size;
    }

    @Override
    public String toString() {
        return level + ":" + size + "@" + price;
    }
}
