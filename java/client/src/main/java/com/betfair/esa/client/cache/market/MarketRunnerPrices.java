package com.betfair.esa.client.cache.market;

import com.betfair.esa.client.cache.util.LevelPriceSize;
import com.betfair.esa.client.cache.util.PriceSize;
import java.util.Collections;
import java.util.List;

public class MarketRunnerPrices {
    List<PriceSize> atl = Collections.emptyList();
    List<PriceSize> atb = Collections.emptyList();
    List<PriceSize> trd = Collections.emptyList();
    List<PriceSize> spb = Collections.emptyList();
    List<PriceSize> spl = Collections.emptyList();

    List<LevelPriceSize> batb = Collections.emptyList();
    List<LevelPriceSize> batl = Collections.emptyList();
    List<LevelPriceSize> bdatb = Collections.emptyList();
    List<LevelPriceSize> bdatl = Collections.emptyList();

    double ltp;
    double spn;
    double spf;
    double tv;

    public List<PriceSize> getAtl() {
        return atl;
    }

    public List<PriceSize> getAtb() {
        return atb;
    }

    public List<PriceSize> getTrd() {
        return trd;
    }

    public List<PriceSize> getSpb() {
        return spb;
    }

    public List<PriceSize> getSpl() {
        return spl;
    }

    public List<LevelPriceSize> getBatb() {
        return batb;
    }

    public List<LevelPriceSize> getBatl() {
        return batl;
    }

    public List<LevelPriceSize> getBdatb() {
        return bdatb;
    }

    public List<LevelPriceSize> getBdatl() {
        return bdatl;
    }

    public double getLtp() {
        return ltp;
    }

    public double getSpn() {
        return spn;
    }

    public double getSpf() {
        return spf;
    }

    public double getTv() {
        return tv;
    }

    @Override
    public String toString() {
        return "MarketRunnerPrices{"
                + "atl="
                + atl
                + ", atb="
                + atb
                + ", trd="
                + trd
                + ", spb="
                + spb
                + ", spl="
                + spl
                + ", batb="
                + batb
                + ", batl="
                + batl
                + ", bdatb="
                + bdatb
                + ", bdatl="
                + bdatl
                + ", ltp="
                + ltp
                + ", spn="
                + spn
                + ", spf="
                + spf
                + ", tv="
                + tv
                + '}';
    }
}
