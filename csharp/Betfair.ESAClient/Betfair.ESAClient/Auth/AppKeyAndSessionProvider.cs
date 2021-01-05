using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Auth {
    /// <summary>
    /// Utility class to provide a session & token via identity SSO
    /// </summary>
    public class AppKeyAndSessionProvider {
        private string _appkey;
        private string _host;
        private string _password;
        private string _username;

        private AppKeyAndSession _session;

        public const string SSO_HOST_COM = "identitysso.betfair.com";
        public const string SSO_HOST_IT = "identitysso.betfair.it";
        public const string SSO_HOST_ES = "identitysso.betfair.es";

        public AppKeyAndSessionProvider(string ssoHost, string appkey, string username, string password) {
            _host = ssoHost;
            _appkey = appkey;
            _username = username;
            _password = password;
            Timeout = TimeSpan.FromSeconds(30);
            //4hrs is normal expire time
            SessionExpireTime = TimeSpan.FromHours(3);
        }

        /// <summary>
        /// AppKey being used
        /// </summary>
        public string Appkey {
            get { return _appkey; }
        }

        /// <summary>
        /// Session expire time (default 3hrs)
        /// </summary>
        public TimeSpan SessionExpireTime { get; set; }

        /// <summary>
        /// Specifies the timeout
        /// </summary>
        public TimeSpan Timeout { get; set; }


        /// <summary>
        /// Constructs a new session token via identity SSO.
        /// Note: These are not cached.
        /// </summary>
        /// <exception cref="InvalidCredentialException">Thrown if authentication response is fail</exception>
        /// <exception cref="IOException">Thrown if authentication call fails</exception>
        /// <returns></returns>
        public AppKeyAndSession GetOrCreateNewSession() {
            if (_session != null) {
                //have a cached session - is it expired
                if ((_session.CreateTime + SessionExpireTime) > DateTime.UtcNow) {
                    Trace.TraceInformation("SSO Login - session not expired - re-using");
                    return _session;
                }
                else {
                    Trace.TraceInformation("SSO Login - session expired");
                }
            }

            Trace.TraceInformation("SSO Login host={0}, appkey={1}, username={2}",
                _host,
                _appkey,
                _username);
            SessionDetails sessionDetails;
            try {
                string uri = string.Format("https://{0}/api/login?username={1}&password={2}",
                    _host,
                    _username,
                    _password);

                HttpWebRequest loginRequest = (HttpWebRequest) WebRequest.Create(uri);
                loginRequest.Headers.Add("X-Application", _appkey);
                loginRequest.Accept = "application/json";
                loginRequest.Method = "POST";
                loginRequest.Timeout = (int) Timeout.TotalMilliseconds;
                WebResponse thePage = loginRequest.GetResponse();
                using (StreamReader reader = new StreamReader(thePage.GetResponseStream())) {
                    string response = reader.ReadToEnd();
                    Trace.TraceInformation("{0}: Response: {1}", _host, response);
                    sessionDetails = JsonConvert.DeserializeObject<SessionDetails>(response);
                }
            }
            catch (Exception e) {
                throw new IOException("SSO Authentication - call failed:", e);
            }

            //got a response - decode
            if (sessionDetails != null && "SUCCESS".Equals(sessionDetails.status)) {
                _session = new AppKeyAndSession(_appkey, sessionDetails.token);
            }
            else {
                throw new InvalidCredentialException("SSO Authentication - response is fail: " + sessionDetails.error);
            }

            return _session;
        }

        /// <summary>
        /// Expires cached token
        /// </summary>
        public void ExpireTokenNow() {
            Trace.TraceInformation("SSO Login - expiring session token now");
            _session = null;
        }
    }

    class SessionDetails {
        public string token;
        public string product;
        public string status;
        public string error;
    }
}
