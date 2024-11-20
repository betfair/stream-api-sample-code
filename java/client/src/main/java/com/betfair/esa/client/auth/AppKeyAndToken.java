package com.betfair.esa.client.auth;

import java.time.Clock;
import java.time.Instant;

/** Created by mulveyj on 07/07/2016. */
public class AppKeyAndToken {

    private final String appKey;
    private final String token;
    private Instant createTime;

    public AppKeyAndToken(String appKey, String token) {
        this.appKey = appKey;
        this.token = token;
        createTime = Instant.now(Clock.systemUTC());
    }

    public Instant getCreateTime() {
        return createTime;
    }

    public void setCreateTime(Instant createTime) {
        this.createTime = createTime;
    }

    public String getToken() {
        return token;
    }

    public String getAppKey() {
        return appKey;
    }
}
