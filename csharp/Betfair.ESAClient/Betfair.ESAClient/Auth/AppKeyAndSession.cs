using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Auth {
    /// <summary>
    /// Wraps an appkey & it's current session
    /// </summary>
    public class AppKeyAndSession {
        public AppKeyAndSession(string appkey, string session) {
            AppKey = appkey;
            Session = session;
            CreateTime = DateTime.UtcNow;
        }

        public string AppKey { get; private set; }
        public DateTime CreateTime { get; private set; }
        public string Session { get; private set; }
    }
}
