package com.betfair.esa.client.protocol;

import com.betfair.esa.swagger.model.RequestMessage;
import com.betfair.esa.swagger.model.StatusMessage;
import java.util.function.Consumer;

/** Created by mulveyj on 07/07/2016. */
public class RequestResponse {
    private final FutureResponse<StatusMessage> future = new FutureResponse<>();
    private final RequestMessage request;
    private final Consumer<RequestResponse> onSuccess;
    private final int id;

    public RequestResponse(int id, RequestMessage request, Consumer<RequestResponse> onSuccess) {
        this.id = id;
        this.request = request;
        this.onSuccess = onSuccess;
    }

    public void processStatusMessage(StatusMessage statusMessage) {
        if (statusMessage.getStatusCode() == StatusMessage.StatusCodeEnum.SUCCESS) {
            if (onSuccess != null) onSuccess.accept(this);
            future.setResponse(statusMessage);
        }
    }

    public FutureResponse<StatusMessage> getFuture() {
        return future;
    }

    public RequestMessage getRequest() {
        return request;
    }

    public int getId() {
        return id;
    }

    public void setException(Exception e) {
        future.setException(e);
    }
}
