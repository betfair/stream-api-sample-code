package com.betfair.esa.client.protocol;

import com.betfair.esa.swagger.model.MarketChange;
import com.betfair.esa.swagger.model.OrderMarketChange;
import com.betfair.esa.swagger.model.StatusMessage;

/** This interface abstracts connection & cache implementation. Created by mulveyj on 07/07/2016. */
public interface ChangeMessageHandler {
    void onOrderChange(ChangeMessage<OrderMarketChange> change);

    void onMarketChange(ChangeMessage<MarketChange> change);

    void onErrorStatusNotification(StatusMessage message);
}
