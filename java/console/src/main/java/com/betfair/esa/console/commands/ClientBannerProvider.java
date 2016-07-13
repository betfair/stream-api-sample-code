package com.betfair.esa.console.commands;

import org.springframework.core.Ordered;
import org.springframework.core.annotation.Order;
import org.springframework.shell.plugin.support.DefaultBannerProvider;
import org.springframework.shell.support.util.OsUtils;
import org.springframework.stereotype.Component;

/**
 * Created by mulveyj on 24/06/2015.
 */
@Component
@Order(Ordered.HIGHEST_PRECEDENCE)
public class ClientBannerProvider extends DefaultBannerProvider {

    public String getBanner() {
        StringBuffer buf = new StringBuffer();
        buf.append("=======================================" + OsUtils.LINE_SEPARATOR);
        buf.append("*                                     *"+ OsUtils.LINE_SEPARATOR);
        buf.append("*    Exchange Streaming Client        *" +OsUtils.LINE_SEPARATOR);
        buf.append("*                                     *"+ OsUtils.LINE_SEPARATOR);
        buf.append("=======================================" + OsUtils.LINE_SEPARATOR);
        buf.append("Version:" + getVersion());
        return buf.toString();
    }

    @Override
    public String getWelcomeMessage() {
        return "Welcome to ESA. For assistance type 'help' then hit ENTER";
    }

    @Override
    public String getVersion() {
        return this.getClass().getPackage().getImplementationVersion();
    }
}
