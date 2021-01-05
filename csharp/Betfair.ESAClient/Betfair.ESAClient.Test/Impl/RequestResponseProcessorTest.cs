using System;
using Betfair.ESAClient.Protocol;
using Betfair.ESASwagger.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Betfair.ESAClient.Test.Impl {
    [TestClass]
    public class RequestResponseProcessorTest {
        private RequestResponseProcessor Processor { get; set; }
        public string LastLine { get; private set; }

        [TestInitialize]
        public void Init() {
            Processor = new RequestResponseProcessor(line => LastLine = line);
        }

        [TestMethod]
        public void TestJsonNoOp() {
            Processor.ReceiveLine("{}");
        }

        [TestMethod]
        public void TestJsonUnknownOp() {
            Processor.ReceiveLine("{\"op\": \"rubbish\"}");
        }

        [TestMethod]
        public void TestOpNotFirst() {
            var msg = (ConnectionMessage) Processor.ReceiveLine("{\"connectionId\":\"aconnid\", \"op\":\"connection\"}");
            Assert.AreEqual("aconnid", msg.ConnectionId);
        }

        [TestMethod]
        public void TestExtraJsonField() {
            var msg = (ConnectionMessage) Processor.ReceiveLine("{\"op\":\"connection\", \"connectionId\":\"aconnid\", \"extraField\":\"extraValue\"}");
            Assert.AreEqual("aconnid", msg.ConnectionId);
        }

        [TestMethod]
        public void TestJsonMissingField() {
            var msg = (ConnectionMessage) Processor.ReceiveLine("{\"op\":\"connection\"}");
            Assert.IsNotNull(msg);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonException), AllowDerivedTypes = true)]
        public void TestInvalidJson() {
            Processor.ReceiveLine("rubbish");
        }

        [TestMethod]
        public void TestConnectionMessageUnwind() {
            //wait and get timeout
            Assert.IsFalse(Processor.ConnectionMessage()
                .Wait(10));

            //process
            var msg = (ConnectionMessage) Processor.ReceiveLine("{\"op\":\"connection\", \"connectionId\":\"aconnid\"}");

            //now unwound
            Assert.IsTrue(Processor.ConnectionMessage()
                .Wait(10));
            Assert.AreEqual("aconnid",
                Processor.ConnectionMessage()
                    .Result.ConnectionId);
            Assert.AreEqual(ConnectionStatus.CONNECTED, Processor.Status);
        }

        [TestMethod]
        public void TestAuthentication() {
            var authTask = Processor.Authenticate(new AuthenticationMessage {Session = "asession", AppKey = "aappkey"});
            Console.WriteLine(LastLine);

            //wait and get timeout
            Assert.IsFalse(authTask.Wait(10));

            Processor.ReceiveLine("{\"op\":\"status\",\"id\":1,\"statusCode\":\"SUCCESS\"");


            //wait and pass
            Assert.IsTrue(authTask.Wait(10));
            Assert.AreEqual(StatusMessage.StatusCodeEnum.Success, authTask.Result.StatusCode);
            Assert.AreEqual(ConnectionStatus.AUTHENTICATED, Processor.Status);
        }

        [TestMethod]
        public void TestAuthenticationFailed() {
            var authTask = Processor.Authenticate(new AuthenticationMessage {Session = "asession", AppKey = "aappkey"});

            //wait and get timeout
            Assert.IsFalse(authTask.Wait(10));

            Processor.ReceiveLine("{\"op\":\"status\",\"id\":1,\"statusCode\":\"FAILURE\", \"errorCode\":\"NO_SESSION\"}");

            //wait and pass
            Assert.IsTrue(authTask.Wait(10));

            Assert.AreEqual(StatusMessage.StatusCodeEnum.Failure, authTask.Result.StatusCode);
            Assert.AreEqual(StatusMessage.ErrorCodeEnum.NoSession, authTask.Result.ErrorCode);
            Assert.AreEqual(ConnectionStatus.STOPPED, Processor.Status);
        }

        [TestMethod]
        public void TestHeartbeat() {
            var authTask = Processor.Heartbeat(new HeartbeatMessage());
            Console.WriteLine(LastLine);

            //wait and get timeout
            Assert.IsFalse(authTask.Wait(10));

            Processor.ReceiveLine("{\"op\":\"status\",\"id\":1,\"statusCode\":\"SUCCESS\"");


            //wait and pass
            Assert.IsTrue(authTask.Wait(10));
            Assert.AreEqual(StatusMessage.StatusCodeEnum.Success, authTask.Result.StatusCode);
        }
    }
}
