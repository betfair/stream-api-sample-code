using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Betfair.ESAClient;
using Betfair.ESAClient.Auth;
using Betfair.ESAClient.Cache;
using Betfair.ESAConsoleApp.Properties;
using ConsoleTables;
using Tectil.NCommand;
using Tectil.NCommand.Contract;

namespace Betfair.ESAConsoleApp {
    internal class Program {
        private static AppKeyAndSessionProvider SessionProvider { get; set; }
        private static ClientCache _clientCache;
        private static bool _traceMarkets;
        private static bool _traceOrders;
        private static string _host = "stream-api-integration.betfair.com";
        private static int _port = 443;

        private static void Main(string[] args) {
            var traceListener = new ConsoleTraceListener();
            traceListener.TraceOutputOptions = TraceOptions.DateTime;
            Trace.Listeners.Add(traceListener);

            var commands = new NCommands();
            commands.Context.Configuration.Notation = ParserNotation.Unix;

            while (true) {
                commands.RunConsole(args);
                StopTracing();
            }
        }

        static Program() {
            if (Settings.Default.AppKey != "") {
                Console.WriteLine("Loading login details:");
                NewSessionProvider(Settings.Default.SsoHost,
                    Settings.Default.AppKey,
                    Settings.Default.UserName,
                    Settings.Default.Password);
            }
        }

        public static void NewSessionProvider(string ssohost, string appkey, string username, string password) {
            var sessionProvider = new AppKeyAndSessionProvider(ssohost,
                appkey,
                username,
                password);
            SessionProvider = sessionProvider;
        }

        public static ClientCache ClientCache {
            get {
                if (_clientCache == null) {
                    if (SessionProvider == null) {
                        Console.Error.WriteLine("****************************************");
                        Console.Error.WriteLine("**** No Login Saved - Use SaveLogin ****");
                        Console.Error.WriteLine("****************************************");
                        throw new ArgumentException("No Login Saved - Use SaveLogin");
                    }

                    var client = new Client(_host, _port, SessionProvider);
                    _clientCache = new ClientCache(client);
                    _clientCache.MarketCache.MarketChanged += OnMarketChanged;
                    _clientCache.OrderCache.OrderMarketChanged += OnOrderMarketChanged;
                }

                return _clientCache;
            }
        }

        private static void OnOrderMarketChanged(object sender, OrderMarketChangedEventArgs e) {
            if (!_traceOrders)
                return;
            PrintOrderMarket(e.Snap);
        }

        private static void PrintOrderMarket(OrderMarketSnap market) {
            Console.WriteLine("Orders (marketid={0}):", market.MarketId);
            ConsoleTable.From(market.OrderMarketRunners.SelectMany(r => r.UnmatchedOrders.Values))
                .Write();
        }

        private static void OnMarketChanged(object sender, MarketChangedEventArgs e) {
            if (!_traceMarkets)
                return;
            PrintMarket(e.Snap);
        }

        private static LevelPriceSize GetLevel(IList<LevelPriceSize> values, int level) {
            return values.ElementAtOrDefault(0) ?? new LevelPriceSize(level, 0, 0);
        }

        private static void PrintMarket(MarketSnap market) {
            var table = new ConsoleTable("market",
                "runner",
                "atb",
                "atl");
            table.AddRow(market.MarketId,
                null,
                null,
                null);
            foreach (var runner in market.MarketRunners.OrderBy(mr => mr.Definition.SortPriority)) {
                var snap = runner.Prices;
                table.AddRow(null,
                    runner.RunnerId.SelectionId,
                    GetLevel(snap.BestAvailableToBack, 0)
                        .Price,
                    GetLevel(snap.BestAvailableToLay, 0)
                        .Price);
                table.AddRow(null,
                    null,
                    GetLevel(snap.BestAvailableToBack, 0)
                        .Size,
                    GetLevel(snap.BestAvailableToLay, 0)
                        .Size);
            }

            table.Write();
        }

        [Command(description: "Save Login - (not encrypted)")]
        public void SaveLogin(
            [Argument(description: "sso host", defaultValue: "identitysso.betfair.com")]
            string ssohost,
            [Argument(description: "app key")]
            string appkey,
            [Argument(description: "user name")]
            string username,
            [Argument(description: "password")]
            string password) {
            Settings.Default.SsoHost = ssohost;
            Settings.Default.AppKey = appkey;
            Settings.Default.UserName = username;
            Settings.Default.Password = password;

            //set the login and verify before saving
            NewSessionProvider(ssohost,
                appkey,
                username,
                password);
            //test it
            SessionProvider.GetOrCreateNewSession();
            Settings.Default.Save();
        }

        [Command(description: "Sets the host & port")]
        public void Host(
            [Argument(defaultValue: "stream-api-integration.betfair.com")]
            string host,
            [Argument(defaultValue: 443)]
            int port) {
            _host = host;
            _port = port;
        }


        [Command(description: "Orders")]
        public void Orders() {
            ClientCache.SubscribeOrders();
        }


        [Command(description: "ListOrders")]
        public void ListOrders() {
            foreach (var market in ClientCache.OrderCache.Markets)
                PrintOrderMarket(market.Snap);
        }

        [Command(description: "Market - subscribes to a market")]
        public void Market(string marketid) {
            ClientCache.SubscribeMarkets(marketid);
        }

        [Command(description: "Market Firehose- subscribes to all markets")]
        public void MarketFireHose() {
            ClientCache.SubscribeMarkets();
        }


        [Command(description: "ListMarkets")]
        public void ListMarkets() {
            foreach (var market in ClientCache.MarketCache.Markets)
                PrintMarket(market.Snap);
        }

        [Command(description: "Stops the connection")]
        public void Stop() {
            ClientCache.Stop();
        }

        [Command(description: "Starts the connection")]
        public void Start() {
            ClientCache.Start();
        }

        [Command(description: "Disconnects the connection")]
        public void Disconnect() {
            ClientCache.Client.Disconnect();
        }

        [Command(description: "Trace Messages (markets and orders)")]
        public void TraceMessages(
            [Argument(defaultValue: 200)]
            int truncate) {
            ClientCache.Client.TraceChangeTruncation = truncate;
        }

        [Command(description: "Trace Market Changes")]
        public void TraceMarkets() {
            _traceMarkets = true;
        }

        [Command(description: "Trace Order Changes")]
        public void TraceOrders() {
            _traceOrders = true;
        }

        public static void StopTracing() {
            _traceOrders = false;
            _traceMarkets = false;
            ClientCache.Client.TraceChangeTruncation = 0;
        }
    }
}
