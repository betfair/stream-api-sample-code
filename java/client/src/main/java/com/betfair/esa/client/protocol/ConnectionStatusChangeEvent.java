package com.betfair.esa.client.protocol;

import java.util.EventObject;

/** Created by mulveyJ on 11/07/2016. */
public class ConnectionStatusChangeEvent extends EventObject {
    private final ConnectionStatus oldStatus;
    private final ConnectionStatus newStatus;

    /**
     * Constructs a prototypical Event.
     *
     * @param source The object on which the Event initially occurred.
     * @throws IllegalArgumentException if source is null.
     */
    public ConnectionStatusChangeEvent(
            Object source, ConnectionStatus oldStatus, ConnectionStatus newStatus) {
        super(source);
        this.oldStatus = oldStatus;
        this.newStatus = newStatus;
    }

    public ConnectionStatus getOldStatus() {
        return oldStatus;
    }

    public ConnectionStatus getNewStatus() {
        return newStatus;
    }
}
