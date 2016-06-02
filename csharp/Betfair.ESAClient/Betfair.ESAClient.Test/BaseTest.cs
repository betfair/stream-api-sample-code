using Betfair.ESAClient.Auth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Test
{
   
    public abstract class BaseTest
    {


        static BaseTest()
        {
            //setup logging
            ConsoleTraceListener traceListener = new ConsoleTraceListener();
            traceListener.TraceOutputOptions = TraceOptions.DateTime;
            Trace.Listeners.Add(traceListener);
        }

        public TestContext TestContext { get; set; }

        public AppKeyAndSessionProvider ValidSessionProvider
        {
            get
            {
                return new AppKeyAndSessionProvider(
                    (string)TestContext.Properties["SsoHost"],
                    (string)TestContext.Properties["AppKey"],
                    (string)TestContext.Properties["UserName"],
                    (string)TestContext.Properties["Password"]);
            }
        }

        public AppKeyAndSessionProvider InvalidHostSessionProvider
        {
            get
            {
                return new AppKeyAndSessionProvider("www.betfair.com", "a", "b", "c");
            }
        }

        public AppKeyAndSessionProvider InvalidLoginSessionProvider
        {
            get
            {
                return new AppKeyAndSessionProvider(AppKeyAndSessionProvider.SSO_HOST_COM, "a", "b", "c");
            }
        }

    }
}
