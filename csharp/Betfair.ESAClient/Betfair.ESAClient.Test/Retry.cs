using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Test
{
    public class Retry
    {
        public static TimeSpan RetryInterval { get; set; }
        public static TimeSpan RetryTimeout { get; set; }

        static Retry()
        {
            RetryInterval = TimeSpan.FromMilliseconds(500);
            RetryTimeout = TimeSpan.FromSeconds(5);
        }

        public static void Action(Action action)
        {
            Exception lastException = null;
            DateTime startTime = DateTime.UtcNow;
            int counter = 0;
            while(DateTime.UtcNow < startTime + RetryTimeout)
            {
                if(counter > 0)
                {
                    Trace.TraceWarning("Retry attempt={0} action={1}", counter, action);
                }
                try
                {
                    action();
                    return;
                }
                catch (Exception e)
                {
                    lastException = e;
                    counter++;
                    Thread.Sleep(RetryInterval);
                }
            }
            throw lastException;
        }
    }
}
