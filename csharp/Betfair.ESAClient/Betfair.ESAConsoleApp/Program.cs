using Betfair.ESAClient;
using Betfair.ESAClient.Auth;
using Betfair.ESAClient.Cache;
using Betfair.ESASwagger.Model;
using ConsoleTables.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tectil.NCommand;
using Tectil.NCommand.Contract;

namespace Betfair.ESAConsoleApp
{
    class Program
    {
        private static AppKeyAndSessionProvider SessionProvider {get; set;}
        private static ClientCache _clientCache;
        private static bool _traceMarkets;
        private static bool _traceOrders;
        private static string _host = "stream-api-integration.betfair.com";
        private static int _port = 443;

        static void Main(string[] args)
        {
            ConsoleTraceListener traceListener = new ConsoleTraceListener();
            traceListener.TraceOutputOptions = TraceOptions.DateTime;
            Trace.Listeners.Add(traceListener);
            
            NCommands commands = new NCommands();
            commands.Context.Configuration.Notation = ParserNotation.Unix;

            while (true)
            {
                commands.RunConsole(args);
                StopTracing();
            }
        }

        static Program()
        {
            if(Properties.Settings.Default.AppKey != "")
            {
                Console.WriteLine("Loading login details:");
                NewSessionProvider(
                    Properties.Settings.Default.SsoHost,
                    Properties.Settings.Default.AppKey,
                    Properties.Settings.Default.UserName,
                    Properties.Settings.Default.Password);
            }
        }

        public static void NewSessionProvider(
            string ssohost,
            string appkey,
            string username,
            string password)
        {
            AppKeyAndSessionProvider sessionProvider = new AppKeyAndSessionProvider(ssohost, appkey, username, password);
            SessionProvider = sessionProvider;
        }

        public static ClientCache ClientCache
        {
            get
            {
                if (_clientCache == null)
                {
                    if (SessionProvider == null)
                    {
                        Console.Error.WriteLine("****************************************");
                        Console.Error.WriteLine("**** No Login Saved - Use SaveLogin ****");
                        Console.Error.WriteLine("****************************************");
                        throw new ArgumentException("No Login Saved - Use SaveLogin");
                    }
                    Client client = new Client(_host, _port, SessionProvider);
                    _clientCache = new ClientCache(client);
                    _clientCache.MarketCache.MarketChanged += OnMarketChanged;
                    _clientCache.OrderCache.OrderMarketChanged += OnOrderMarketChanged;
                }
                return _clientCache;
            }
        }

        private static void OnOrderMarketChanged(object sender, OrderMarketChangedEventArgs e)
        {
            if (!_traceOrders) return;
            PrintOrderMarket(e.Snap);
        }

        private static void PrintOrderMarket(OrderMarketSnap market)
        {
            Console.WriteLine("Orders (marketid={0}):", market.MarketId);
            ConsoleTable.From<Order>(market.OrderMarketRunners.SelectMany(r => r.UnmatchedOrders.Values))
                .Write();
        }

        private static void OnMarketChanged(object sender, MarketChangedEventArgs e)
        {
            if(!_traceMarkets) return;
            PrintMarket(e.Snap);
        }

        private static LevelPriceSize GetLevel(IList<LevelPriceSize> values, int level)
        {
            return values.ElementAtOrDefault(0) ?? new LevelPriceSize(level, 0, 0);
        }

        private static void PrintMarket(MarketSnap market)
        {
            ConsoleTable table = new ConsoleTable("market", "runner", "atb", "atl");
            table.AddRow(market.MarketId, null, null, null);
            foreach (MarketRunnerSnap runner in market.MarketRunners.OrderBy(mr => mr.Definition.SortPriority))
            {
                MarketRunnerPrices snap = runner.Prices;
                table.AddRow(null, runner.RunnerId.SelectionId, GetLevel(snap.BestAvailableToBack, 0).Price, GetLevel(snap.BestAvailableToLay, 0).Price);
                table.AddRow(null, null, GetLevel(snap.BestAvailableToBack, 0).Size, GetLevel(snap.BestAvailableToLay, 0).Size);
            }
            table.Write();
        }

        [Command(description: "Save Login - (not encrypted)")]
        public void SaveLogin(
            [Argument(description: "sso host", defaultValue: "identitysso.betfair.com")]string ssohost,
            [Argument(description: "app key")]string appkey, 
            [Argument(description: "user name")]string username, 
            [Argument(description: "password")]string password)
        {
            Properties.Settings.Default.SsoHost = ssohost;
            Properties.Settings.Default.AppKey = appkey;
            Properties.Settings.Default.UserName = username;
            Properties.Settings.Default.Password = password;

            //set the login and verify before saving
            NewSessionProvider(ssohost, appkey, username, password);
            //test it
            SessionProvider.GetOrCreateNewSession();
            Properties.Settings.Default.Save();
        }

        [Command(description: "Sets the host & port")]
        public void Host([Argument(defaultValue: "stream-api-integration.betfair.com")]string host, [Argument(defaultValue: 443)]int port)
        {
            _host = host;
            _port = port;
        }


        [Command(description: "Orders")]
        public void Orders()
        {
            ClientCache.SubscribeOrders();
        }


        [Command(description: "ListOrders")]
        public void ListOrders()
        {
            foreach(OrderMarket market in ClientCache.OrderCache.Markets)
            {
                PrintOrderMarket(market.Snap);
            }
        }

        [Command(description: "Market - subscribes to a market")]
        public void Market(string marketid)
        {
            ClientCache.SubscribeMarkets(marketid);
        }

        [Command(description: "Market Firehose- subscribes to all markets")]
        public void MarketFireHose()
        {
            ClientCache.SubscribeMarkets();
        }


        [Command(description: "ListMarkets")]
        public void ListMarkets()
        {
            foreach(Market market in ClientCache.MarketCache.Markets){
                PrintMarket(market.Snap);
            }
        }

        [Command(description: "Stops the connection")]
        public void Stop()
        {
            ClientCache.Stop();
        }

        [Command(description: "Starts the connection")]
        public void Start()
        {
            ClientCache.Start();
        }

        [Command(description: "Disconnects the connection")]
        public void Disconnect()
        {
            ClientCache.Client.Disconnect();
        }

        [Command(description: "Trace Messages (markets and orders)")]
        public void TraceMessages([Argument(defaultValue: 200)]int truncate)
        {
            ClientCache.Client.TraceChangeTruncation = truncate;
        }

        [Command(description: "Trace Market Changes")]
        public void TraceMarkets()
        {
            _traceMarkets = true;
        }

        [Command(description: "Trace Order Changes")]
        public void TraceOrders()
        {
            _traceOrders = true;
        }

        public static void StopTracing()
        {
            _traceOrders = false;
            _traceMarkets = false;
            ClientCache.Client.TraceChangeTruncation = 0;
        }
    }
}
