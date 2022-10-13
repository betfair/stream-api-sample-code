package com.betfair.esa.client.protocol;

import java.util.EventListener;

/** Created by mulveyJ on 11/07/2016. */
public interface ConnectionStatusListener extends EventListener {
    void connectionStatusChange(ConnectionStatusChangeEvent statusChangeEvent);
}
