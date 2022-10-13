package com.betfair.esa.client.protocol;

import com.betfair.esa.swagger.model.ConnectionMessage;
import com.betfair.esa.swagger.model.MarketChangeMessage;
import com.betfair.esa.swagger.model.OrderChangeMessage;
import com.betfair.esa.swagger.model.StatusMessage;
import com.fasterxml.jackson.annotation.JsonSubTypes;
import com.fasterxml.jackson.annotation.JsonTypeInfo;

@JsonTypeInfo(
        use = JsonTypeInfo.Id.NAME,
        include = JsonTypeInfo.As.PROPERTY,
        property = "op",
        visible = true)
@JsonSubTypes({
    @JsonSubTypes.Type(value = ConnectionMessage.class, name = "connection"),
    @JsonSubTypes.Type(value = StatusMessage.class, name = "status"),
    @JsonSubTypes.Type(value = MarketChangeMessage.class, name = "mcm"),
    @JsonSubTypes.Type(value = OrderChangeMessage.class, name = "ocm"),
})
interface MixInResponseMessage {}
