package com.betfair.esa.client.auth;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.sun.jersey.api.client.Client;
import com.sun.jersey.api.client.ClientResponse;
import com.sun.jersey.api.client.WebResource;
import java.io.IOException;
import java.net.URLEncoder;
import java.nio.charset.StandardCharsets;
import java.time.Clock;
import java.time.Duration;
import java.time.Instant;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/** Created by hoszua on 07/07/2016. */
public class AppKeyAndSessionProvider {

    private final Logger logger = LoggerFactory.getLogger(getClass().getName());

    private final Duration timeout;
    private final Duration sessionExpireTime;

    private final String appkey;
    private final String host;
    private final String password;
    private final String username;

    private AppKeyAndSession session;

    public static final String SSO_HOST_COM = "identitysso.betfair.com";
    public static final String SSO_HOST_IT = "identitysso.betfair.it";
    public static final String SSO_HOST_ES = "identitysso.betfair.es";
    private ObjectMapper mapper = new ObjectMapper();

    public AppKeyAndSessionProvider(
            String ssoHost, String appkey, String username, String password) {
        this.host = ssoHost;
        this.appkey = appkey;
        this.username = username;
        this.password = password;
        this.timeout = Duration.ofSeconds(30);
        // 4hrs is normal expire time
        this.sessionExpireTime = Duration.ofHours(3);
    }

    /**
     * Constructs a new session token via identity SSO. Note: These are not cached.
     *
     * @return
     * @throws IOException Thrown if authentication call fails
     * @throws InvalidCredentialException Thrown if authentication response is fail
     */
    public AppKeyAndSession getOrCreateNewSession() throws IOException, InvalidCredentialException {
        if (session != null) {
            // have a cached session - is it expired
            if ((session.getCreateTime().plus(sessionExpireTime))
                    .isAfter(Instant.now(Clock.systemUTC()))) {
                logger.info("SSO Login - session not expired - re-using");
                return session;
            } else {
                logger.info("SSO Login - session expired");
            }
        }
        logger.info("SSO Login host={}, appkey={}, username={}", host, appkey, username);
        SessionDetails sessionDetails;
        try {
            String uri =
                    String.format(
                            "https://%s/api/login?username=%s&password=%s",
                            host,
                            URLEncoder.encode(username, StandardCharsets.UTF_8),
                            URLEncoder.encode(password, StandardCharsets.UTF_8));

            Client client = Client.create();
            client.setConnectTimeout((int) (timeout.getSeconds() * 1000));
            WebResource webResource = client.resource(uri);

            ClientResponse clientResponse =
                    webResource
                            .accept("application/json")
                            .header("X-Application", appkey)
                            .header("Content-Type", "application/x-www-form-urlencoded")
                            .post(ClientResponse.class);

            mapper = new ObjectMapper();
            sessionDetails =
                    mapper.readValue(clientResponse.getEntityInputStream(), SessionDetails.class);

            logger.info("{}: Response: {}", host, sessionDetails);

        } catch (Exception e) {
            throw new IOException("SSO Authentication - call failed:", e);
        }

        // got a response - decode
        if (sessionDetails != null && "SUCCESS".equals(sessionDetails.status)) {
            session = new AppKeyAndSession(appkey, sessionDetails.token);
        } else {
            throw new InvalidCredentialException(
                    "SSO Authentication - response is fail: " + sessionDetails.error);
        }
        return session;
    }

    /** Expires cached token */
    public void expireTokenNow() {
        logger.info("SSO Login - expiring session token now");
        session = null;
    }

    public static class SessionDetails {
        public String token;
        public String product;
        public String status;
        public String error;
    }

    public Duration getTimeout() {
        return timeout;
    }

    public Duration getSessionExpireTime() {
        return sessionExpireTime;
    }
}
