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

        public string SsoHost { get { return (string)TestContext.Properties["SsoHost"]; } }
        public string AppKey { get { return (string)TestContext.Properties["AppKey"]; } }
        public string UserName { get { return (string)TestContext.Properties["UserName"]; } }
        public string Password { get { return (string)TestContext.Properties["Password"]; } }

        public AppKeyAndSessionProvider ValidSessionProvider
        {
            get
            {
                return new AppKeyAndSessionProvider(
                    SsoHost,
                    AppKey,
                    UserName,
                    Password);
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
                return new AppKeyAndSessionProvider(AppKeyAndSessionProvider.SSO_HOST_COM, "appkey", "username", "password");
            }
        }

    }
}
