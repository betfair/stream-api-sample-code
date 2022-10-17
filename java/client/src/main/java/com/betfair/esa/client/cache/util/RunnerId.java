package com.betfair.esa.client.cache.util;

import java.util.Objects;

public record RunnerId(long selectionId, Double handicap) {

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        RunnerId runnerId = (RunnerId) o;

        if (selectionId != runnerId.selectionId) return false;
        return Objects.equals(handicap, runnerId.handicap);
    }

    @Override
    public int hashCode() {
        int result = (int) (selectionId ^ (selectionId >>> 32));
        result = 31 * result + (handicap != null ? handicap.hashCode() : 0);
        return result;
    }

    @Override
    public String toString() {
        return "RunnerId{" + "selectionId=" + selectionId + ", handicap=" + handicap + '}';
    }
}
