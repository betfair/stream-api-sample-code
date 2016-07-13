package com.betfair.esa.console;

/**
 * Created by HoszuA on 11/07/2016.
 */

import org.springframework.shell.Bootstrap;

import java.io.IOException;

/* If you are running this in an IDE please be aware of console issues:
        * Spring Shell uses Jline. You will have to edit the run configuration inside your Intellij and add the vm options arguments as follow:
        *  Unix machines:
        *  -Djline.terminal=org.springframework.shell.core.IdeTerminal
        *  On Windows machines:
        * -Djline.WindowsTerminal.directConsole=false -Djline.terminal=jline.UnsupportedTerminal
        */
public final class ClientMain {
    private ClientMain() {}

    public static void main(String[] args) throws IOException {
        Bootstrap.main(args);
    }
}
