package com.betfair.esa.client.auth;

import java.time.Clock;
import java.time.Instant;

/** Created by mulveyj on 07/07/2016. */
public class AppKeyAndSession {

    private final String appKey;
    private final String session;
    private Instant createTime;

    public AppKeyAndSession(String appKey, String session) {
        this.appKey = appKey;
        this.session = session;
        createTime = Instant.now(Clock.systemUTC());
    }

    public Instant getCreateTime() {
        return createTime;
    }

    public void setCreateTime(Instant createTime) {
        this.createTime = createTime;
    }

    public String getSession() {
        return session;
    }

    public String getAppKey() {
        return appKey;
    }
}
