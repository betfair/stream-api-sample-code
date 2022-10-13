package com.betfair.esa.client;

import com.betfair.esa.client.auth.InvalidCredentialException;
import com.betfair.esa.client.cache.market.MarketCache;
import com.betfair.esa.client.cache.order.OrderCache;
import com.betfair.esa.client.protocol.ChangeMessage;
import com.betfair.esa.client.protocol.ChangeMessageHandler;
import com.betfair.esa.client.protocol.ConnectionException;
import com.betfair.esa.client.protocol.ConnectionStatus;
import com.betfair.esa.client.protocol.ConnectionStatusListener;
import com.betfair.esa.client.protocol.StatusException;
import com.betfair.esa.swagger.model.MarketChange;
import com.betfair.esa.swagger.model.MarketDataFilter;
import com.betfair.esa.swagger.model.MarketFilter;
import com.betfair.esa.swagger.model.MarketSubscriptionMessage;
import com.betfair.esa.swagger.model.OrderMarketChange;
import com.betfair.esa.swagger.model.OrderSubscriptionMessage;
import com.betfair.esa.swagger.model.StatusMessage;
import java.util.Arrays;

/** Created by mulveyj on 07/07/2016. */
public class ClientCache implements ChangeMessageHandler {
    private final MarketCache marketCache = new MarketCache();
    private final OrderCache orderCache = new OrderCache();
    private final Client client;

    public ClientCache(Client client) {
        this.client = client;
        client.setChangeHandler(this);
    }

    public void addConnectionStatusListener(ConnectionStatusListener listener) {
        client.addConnectionStatusListener(listener);
    }

    public void removeConnectionStatusListener(ConnectionStatusListener listener) {
        client.removeConnectionStatusListener(listener);
    }

    public ConnectionStatus getStatus() {
        return client.getStatus();
    }

    public void setHeartbeatMs(Long heartbeatMs) {
        client.setHeartbeatMs(heartbeatMs);
    }

    public Long getHeartbeatMs() {
        return client.getHeartbeatMs();
    }

    public void setConflateMs(Long conflateMs) {
        client.setConflateMs(conflateMs);
    }

    public Long getConflateMs() {
        return client.getConflateMs();
    }

    public void setMarketDataFilter(MarketDataFilter marketDataFilter) {
        client.setMarketDataFilter(marketDataFilter);
    }

    public MarketDataFilter getMarketDataFilter() {
        return client.getMarketDataFilter();
    }

    public Client getClient() {
        return client;
    }

    public MarketCache getMarketCache() {
        return marketCache;
    }

    public OrderCache getOrderCache() {
        return orderCache;
    }

    /** Subscribe to all orders. (starting the client if needed). */
    public void subscribeOrders()
            throws InvalidCredentialException, StatusException, ConnectionException {
        subscribeOrders(new OrderSubscriptionMessage());
    }

    /** Explict order subscription. (starting the client if needed). */
    public void subscribeOrders(OrderSubscriptionMessage subscription)
            throws InvalidCredentialException, StatusException, ConnectionException {
        client.start();
        client.orderSubscription(subscription);
    }

    /** Subscribe to the specified market ids. (starting the client if needed). */
    public void subscribeMarkets(String... markets)
            throws InvalidCredentialException, StatusException, ConnectionException {
        MarketFilter marketFilter = new MarketFilter();
        marketFilter.setMarketIds(Arrays.asList(markets));
        subscribeMarkets(marketFilter);
    }

    /**
     * Subscribe to the specified markets (matching your filter). (starting the client if needed).
     */
    public void subscribeMarkets(MarketFilter marketFilter)
            throws InvalidCredentialException, StatusException, ConnectionException {
        MarketSubscriptionMessage marketSubscriptionMessage = new MarketSubscriptionMessage();
        marketSubscriptionMessage.setMarketFilter(marketFilter);
        subscribeMarkets(marketSubscriptionMessage);
    }

    /// <summary>
    /// Explicit order subscripion. (starting the client if needed).
    /// </summary>
    /// <param name="subscription"></param>
    public void subscribeMarkets(MarketSubscriptionMessage subscription)
            throws InvalidCredentialException, StatusException, ConnectionException {
        client.start();
        client.marketSubscription(subscription);
    }

    @Override
    public void onOrderChange(ChangeMessage<OrderMarketChange> change) {
        orderCache.onOrderChange(change);
    }

    @Override
    public void onMarketChange(ChangeMessage<MarketChange> change) {
        marketCache.onMarketChange(change);
    }

    @Override
    public void onErrorStatusNotification(StatusMessage message) {}
}
