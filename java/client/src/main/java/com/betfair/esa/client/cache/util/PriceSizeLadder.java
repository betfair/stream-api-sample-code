package com.betfair.esa.client.cache.util;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

public class PriceSizeLadder {
    private final Map<Double, PriceSize> priceToSize;
    private List<PriceSize> snap = Collections.emptyList();

    public static PriceSizeLadder newBack() {
        return new PriceSizeLadder(Comparator.reverseOrder());
    }

    public static PriceSizeLadder newLay() {
        return new PriceSizeLadder(Comparator.naturalOrder());
    }

    private PriceSizeLadder(Comparator<Double> comparator) {
        priceToSize = new TreeMap<>(comparator);
    }

    public List<PriceSize> onPriceChange(boolean isImage, List<List<Double>> prices) {
        if (isImage) {
            priceToSize.clear();
        }
        if (prices != null) {
            for (List<Double> price : prices) {
                PriceSize priceSize = new PriceSize(price);
                if (priceSize.getSize() == 0.0) {
                    priceToSize.remove(priceSize.getPrice());
                } else {
                    priceToSize.put(priceSize.getPrice(), priceSize);
                }
            }
        }
        if (isImage || prices != null) {
            // update snap on image or if we had cell changes
            snap = new ArrayList<>(priceToSize.values());
        }
        return snap;
    }

    @Override
    public String toString() {
        return "{" + priceToSize.values() + '}';
    }
}
