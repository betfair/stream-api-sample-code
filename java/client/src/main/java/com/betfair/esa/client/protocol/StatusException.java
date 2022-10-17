package com.betfair.esa.client.protocol;

import com.betfair.esa.swagger.model.StatusMessage;

/** Created by mulveyj on 07/07/2016. */
public class StatusException extends Exception {

    private final StatusMessage.ErrorCodeEnum errorCode;
    private final String errorMessage;

    public StatusException(StatusMessage message) {
        super(message.getErrorCode() + ": " + message.getErrorMessage());
        errorCode = message.getErrorCode();
        errorMessage = message.getErrorMessage();
    }

    public StatusMessage.ErrorCodeEnum getErrorCode() {
        return errorCode;
    }

    public String getErrorMessage() {
        return errorMessage;
    }
}
