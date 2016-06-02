using Betfair.ESAClient.Auth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Test.Auth
{
    [TestClass]
    public class AppKeyAndSessionProviderTest : BaseTest
    {

        [TestMethod]
        public void TestValidSession()
        {
            AppKeyAndSession session = ValidSessionProvider.GetOrCreateNewSession();
            Assert.IsNotNull(session);
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void TestInvalidHost()
        {
            InvalidHostSessionProvider.GetOrCreateNewSession();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCredentialException))]
        public void TestInvalidLogin()
        {
            InvalidLoginSessionProvider.GetOrCreateNewSession();
        }
    }
}
