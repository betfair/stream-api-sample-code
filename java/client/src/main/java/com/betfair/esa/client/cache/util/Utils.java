package com.betfair.esa.client.cache.util;

/** Utils class */
public class Utils {

    public static double selectPrice(boolean isImage, double currentPrice, Double newPrice) {
        if (isImage) {
            return newPrice == null ? 0.0 : newPrice;
        } else {
            return newPrice == null ? currentPrice : newPrice;
        }
    }
}
