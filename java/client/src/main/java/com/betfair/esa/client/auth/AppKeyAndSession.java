package com.betfair.esa.client.auth;

import java.time.Clock;
import java.time.Instant;
import java.time.LocalDateTime;
import java.util.Calendar;
import java.util.Date;
import java.util.TimeZone;

/**
 * Created by mulveyj on 07/07/2016.
 */
public class AppKeyAndSession {

    private String appkey;
    private String session;
    private Instant createTime;


    public AppKeyAndSession(String appkey, String session) {
        this.appkey = appkey;
        this.session = session;
        createTime= Instant.now(Clock.systemUTC());
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

    public String getAppkey() {
        return appkey;
    }
}
