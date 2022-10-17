package com.betfair.esa.client.protocol;

import java.util.concurrent.FutureTask;

/** Created by mulveyj on 07/07/2016. */
public class FutureResponse<T> extends FutureTask<T> {
    private static final Runnable NULL = () -> {};

    public FutureResponse() {
        super(NULL, null);
    }

    public void setResponse(T response) {
        set(response);
    }

    public void setException(Throwable t) {
        super.setException(t);
    }
}
