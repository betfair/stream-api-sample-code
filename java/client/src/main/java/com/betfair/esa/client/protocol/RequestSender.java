package com.betfair.esa.client.protocol;

/** Created by mulveyj on 07/07/2016. */
public interface RequestSender {

    void sendLine(String line) throws ConnectionException;
}
