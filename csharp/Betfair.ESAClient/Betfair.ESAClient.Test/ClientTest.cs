using System;
using Betfair.ESAClient.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Betfair.ESAClient.Test {
    [TestClass]
    public class ClientTest : BaseTest {
        private Client Client;

        [TestInitialize]
        public void TestInitialize() {
            Client = new Client("stream-api-integration.betfair.com", 443, ValidSessionProvider);
        }

        [TestCleanup]
        public void TestCleanup() {
            Client.Stop();
        }


        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public void TestInvalidHost() {
            var invalidClient = new Client("www.betfair.com", 443, ValidSessionProvider);
            invalidClient.Timeout = TimeSpan.FromMilliseconds(100);
            invalidClient.Start();
        }

        [TestMethod]
        public void TestStartStop() {
            Assert.AreEqual(ConnectionStatus.STOPPED, Client.Status);
            Client.Start();
            Assert.AreEqual(ConnectionStatus.AUTHENTICATED, Client.Status);
            Client.Stop();
            Assert.AreEqual(ConnectionStatus.STOPPED, Client.Status);
        }

        [TestMethod]
        public void TestStartHearbeatStop() {
            Client.Start();
            Client.Heartbeat();
            Client.Stop();
        }

        [TestMethod]
        public void TestReentrantStartStop() {
            Client.Start();
            Assert.AreEqual(ConnectionStatus.AUTHENTICATED, Client.Status);
            Client.Heartbeat();
            Client.Stop();
            Assert.AreEqual(ConnectionStatus.STOPPED, Client.Status);

            Client.Start();
            Assert.AreEqual(ConnectionStatus.AUTHENTICATED, Client.Status);
            Client.Heartbeat();
            Client.Stop();
            Assert.AreEqual(ConnectionStatus.STOPPED, Client.Status);
        }

        [TestMethod]
        public void TestDoubleStartStop() {
            Client.Start();
            Client.Start();
            Assert.AreEqual(ConnectionStatus.AUTHENTICATED, Client.Status);
            Client.Heartbeat();
            Client.Stop();
            Client.Stop();
            Assert.AreEqual(ConnectionStatus.STOPPED, Client.Status);
        }

        [TestMethod]
        public void TestDisconnectWithNoAutoReconnect() {
            Client.AutoReconnect = false;
            Client.Start();
            Assert.AreEqual(ConnectionStatus.AUTHENTICATED, Client.Status);
            Client.Heartbeat();

            //socket disconnect
            Client.Disconnect();

            //wait for status to go disconnected
            Retry.Action(() => Assert.AreEqual(ConnectionStatus.STOPPED, Client.Status));
        }

        [TestMethod]
        public void TestDisconnectWithAutoReconnect() {
            Client.ReconnectBackOff = TimeSpan.FromMilliseconds(100);
            Client.Start();
            Assert.AreEqual(ConnectionStatus.AUTHENTICATED, Client.Status);
            Client.Heartbeat();

            //socket disconnect
            Assert.AreEqual(0, Client.DisconnectCounter);
            Client.Disconnect();
            Assert.AreEqual(0, Client.DisconnectCounter);

            //retry until connected
            Retry.Action(() => Client.Heartbeat());
            Assert.AreEqual(ConnectionStatus.AUTHENTICATED, Client.Status);
            Assert.AreEqual(1, Client.DisconnectCounter);
        }
    }
}
