package com.betfair.esa.client.cache.order;

import com.betfair.esa.client.cache.util.OrderMarketSnap;
import com.betfair.esa.client.protocol.ChangeMessage;
import com.betfair.esa.swagger.model.OrderMarketChange;
import java.util.ArrayList;
import java.util.EventListener;
import java.util.EventObject;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.CopyOnWriteArrayList;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class OrderCache {
    private static final Logger LOG = LoggerFactory.getLogger(OrderCache.class);

    private final Map<String, OrderMarket> markets = new ConcurrentHashMap<>();
    private final CopyOnWriteArrayList<OrderMarketChangeListener> orderMarketChangeListeners =
            new CopyOnWriteArrayList<>();
    private final CopyOnWriteArrayList<BatchOrderMarketsChangeListener>
            batchOrderMarketsChangeListeners = new CopyOnWriteArrayList<>();

    /** Wether order markets are automatically removed on close (default is true) */
    public boolean isOrderMarketRemovedOnClose = true;

    public void onOrderChange(ChangeMessage<OrderMarketChange> changeMessage) {

        if (changeMessage.isStartOfNewSubscription()) {
            markets.clear();
        }

        if (changeMessage.getItems() != null) {
            List<OrderMarketChangeEvent> batch =
                    (batchOrderMarketsChangeListeners.size() == 0)
                            ? null
                            : new ArrayList<>(changeMessage.getItems().size());

            for (OrderMarketChange change : changeMessage.getItems()) {
                boolean isImage = Boolean.TRUE.equals(change.isFullImage());
                if (isImage) {
                    // Clear market from cache if it is being re-imaged
                    markets.remove(change.getId());
                }

                OrderMarket orderMarket = onOrderMarketChange(change);

                if (isOrderMarketRemovedOnClose && orderMarket.isClosed()) {
                    // remove on close
                    markets.remove(orderMarket.getMarketId());
                }

                if (batch != null || orderMarketChangeListeners.size() != 0) {
                    OrderMarketChangeEvent orderMarketChangeEvent =
                            new OrderMarketChangeEvent(this);
                    orderMarketChangeEvent.setChange(change);
                    orderMarketChangeEvent.setOrderMarket(orderMarket);

                    if (orderMarketChangeListeners != null) {
                        dispatchOrderMarketChanged(orderMarketChangeEvent);
                    }

                    if (batch != null) {
                        batch.add(orderMarketChangeEvent);
                    }
                }
            }

            if (batch != null) {
                dispatchBatchMarketOrderChanged(new BatchOrderMarketsChangeEvent(batch));
            }
        }
    }

    private void dispatchBatchMarketOrderChanged(
            BatchOrderMarketsChangeEvent batchOrderMarketsChangeEvent) {
        try {
            batchOrderMarketsChangeListeners.forEach(
                    l -> l.batchOrderMarketChange(batchOrderMarketsChangeEvent));
        } catch (Exception e) {
            LOG.error("Exception from batch event listener", e);
        }
    }

    private void dispatchOrderMarketChanged(OrderMarketChangeEvent orderMarketChangeEvent) {
        try {
            orderMarketChangeListeners.forEach(l -> l.orderChange(orderMarketChangeEvent));
        } catch (Exception ex) {
            LOG.error("Exception from event listener", ex);
        }
    }

    private OrderMarket onOrderMarketChange(OrderMarketChange orderMarketChange) {
        OrderMarket orderMarket =
                markets.computeIfAbsent(
                        orderMarketChange.getId(),
                        key -> new OrderMarket(this, orderMarketChange.getId()));

        orderMarket.onOrderMarketChange(orderMarketChange);
        return orderMarket;
    }

    public boolean isOrderMarketRemovedOnClose() {
        return isOrderMarketRemovedOnClose;
    }

    public void setOrderMarketRemovedOnClose(boolean orderMarketRemovedOnClose) {
        isOrderMarketRemovedOnClose = orderMarketRemovedOnClose;
    }

    public class OrderMarketChangeEvent extends EventObject {

        private OrderMarketChange orderMarketChange;
        private OrderMarket orderMarket;

        /**
         * Constructs a prototypical Event.
         *
         * @param source The object on which the Event initially occurred.
         * @throws IllegalArgumentException if source is null.
         */
        public OrderMarketChangeEvent(Object source) {
            super(source);
        }

        public OrderMarketSnap snap() {
            return orderMarket.getOrderMarketSnap();
        }

        public OrderMarketChange getChange() {
            return orderMarketChange;
        }

        public void setChange(OrderMarketChange change) {
            this.orderMarketChange = change;
        }

        public void setOrderMarket(OrderMarket orderMarket) {
            this.orderMarket = orderMarket;
        }
    }

    public class BatchOrderMarketsChangeEvent extends EventObject {

        public List<OrderMarketChangeEvent> changes;
        /**
         * Constructs a prototypical Event.
         *
         * @param source The object on which the Event initially occurred.
         * @throws IllegalArgumentException if source is null.
         */
        public BatchOrderMarketsChangeEvent(Object source) {
            super(source);
        }

        public List<OrderMarketChangeEvent> getChanges() {
            return changes;
        }

        public void setChanges(List<OrderMarketChangeEvent> changes) {
            this.changes = changes;
        }
    }

    public interface OrderMarketChangeListener extends EventListener {
        void orderChange(OrderMarketChangeEvent orderChangeEvent);
    }

    public interface BatchOrderMarketsChangeListener extends EventListener {
        void batchOrderMarketChange(BatchOrderMarketsChangeEvent batchOrderMarketsChangeEvent);
    }

    public void addOrderMarketChangeListener(OrderMarketChangeListener listener) {
        orderMarketChangeListeners.add(listener);
    }

    public void addBatchOrderMarketChangeListener(BatchOrderMarketsChangeListener listener) {
        batchOrderMarketsChangeListeners.add(listener);
    }

    public void removeBatchMarketChangeListener(BatchOrderMarketsChangeListener listener) {
        batchOrderMarketsChangeListeners.remove(listener);
    }

    public Iterable<OrderMarket> getOrderMarkets() {
        return markets.values();
    }

    public int getNumberOfOrderMarkets() {
        return markets.size();
    }
}
