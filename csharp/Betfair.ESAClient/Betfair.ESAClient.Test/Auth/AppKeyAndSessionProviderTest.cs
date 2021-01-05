using System.IO;
using System.Security.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Betfair.ESAClient.Test.Auth {
    [TestClass]
    public class AppKeyAndSessionProviderTest : BaseTest {
        [TestMethod]
        public void TestValidSession() {
            var session = ValidSessionProvider.GetOrCreateNewSession();
            Assert.IsNotNull(session);
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void TestInvalidHost() {
            InvalidHostSessionProvider.GetOrCreateNewSession();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCredentialException))]
        public void TestInvalidLogin() {
            InvalidLoginSessionProvider.GetOrCreateNewSession();
        }
    }
}
