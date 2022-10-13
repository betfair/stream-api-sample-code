package com.betfair.esa.client.cache.util;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

/** A level price size ladder with copy on write snapshot */
public class LevelPriceSizeLadder {
    private final Map<Integer, LevelPriceSize> levelToPriceSize = new TreeMap<>();
    private List<LevelPriceSize> snap = Collections.emptyList();

    public List<LevelPriceSize> onPriceChange(boolean isImage, List<List<Double>> prices) {
        if (isImage) {
            // image is replace
            levelToPriceSize.clear();
        }

        if (prices != null) {
            // changes to apply
            for (List<Double> price : prices) {
                LevelPriceSize levelPriceSize = new LevelPriceSize(price);
                levelToPriceSize.put(levelPriceSize.getLevel(), levelPriceSize);
            }
        }

        if (isImage || prices != null) {
            // update snap on image or if we had cell changes
            snap = new ArrayList<>(levelToPriceSize.values());
        }
        return snap;
    }
}
