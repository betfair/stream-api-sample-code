package com.betfair.esa.client;

import com.betfair.esa.client.auth.AppKeyAndSessionProvider;
import org.testng.annotations.BeforeClass;

/**
 * Created by mulveyj on 08/07/2016.
 */
public class BaseTest {

    private static String appKey;
    private static String userName;
    private static String password;

    @BeforeClass
    public static void beforeClass(){
        appKey = getSystemProperty("AppKey");
        userName = getSystemProperty("UserName");
        password = getSystemProperty("Password");
    }

    private static String getSystemProperty(String appKey) {
        String value = System.getProperty(appKey);
        if(value == null){
            throw new IllegalArgumentException("System property '{}' must be set for tests to run");
        }
        return value;
    }

    public static String getAppKey() {
        return appKey;
    }

    public static String getUserName() {
        return userName;
    }

    public static String getPassword() {
        return password;
    }

    public AppKeyAndSessionProvider getValidSessionProvider(){
        return new AppKeyAndSessionProvider(
                AppKeyAndSessionProvider.SSO_HOST_COM,
                appKey,
                userName,
                password);
    }

    public AppKeyAndSessionProvider getInvalidHostSessionProvider(){
        return new AppKeyAndSessionProvider(
                "www.betfair.com",
                "a",
                "b",
                "c");
    }

    public AppKeyAndSessionProvider getInvalidLoginSessionProvider(){
        return new AppKeyAndSessionProvider(
                AppKeyAndSessionProvider.SSO_HOST_COM,
                "a",
                "b",
                "c");
    }

}
