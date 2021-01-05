using System;
using System.Diagnostics;
using System.Threading;

namespace Betfair.ESAClient.Test {
    public class Retry {
        public static TimeSpan RetryInterval { get; set; }
        public static TimeSpan RetryTimeout { get; set; }

        static Retry() {
            RetryInterval = TimeSpan.FromMilliseconds(500);
            RetryTimeout = TimeSpan.FromSeconds(5);
        }

        public static void Action(Action action) {
            Exception lastException = null;
            var startTime = DateTime.UtcNow;
            var counter = 0;
            while (DateTime.UtcNow < startTime + RetryTimeout) {
                if (counter > 0)
                    Trace.TraceWarning("Retry attempt={0} action={1}", counter, action);
                try {
                    action();
                    return;
                }
                catch (Exception e) {
                    lastException = e;
                    counter++;
                    Thread.Sleep(RetryInterval);
                }
            }

            throw lastException;
        }
    }
}
