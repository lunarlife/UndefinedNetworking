using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Networking;
using Networking.Events;
using Networking.Loggers;
using UndefinedNetworking;
using UndefinedNetworking.GameEngine;
using UndefinedNetworking.Packets.Server;
using UndefinedServer.Chats;
using UndefinedServer.Commands;
using UndefinedServer.Events;
using UndefinedServer.Exceptions;
using UndefinedServer.GameEngine;
using UndefinedServer.Gameplay;
using UndefinedServer.Loggers;
using UndefinedServer.Plugins;
using Utils;
using Utils.AsyncOperations;
using Utils.Events;
using Version = Utils.Version;

namespace UndefinedServer
{
    public class Undefined
    {
        private static Undefined? _instance;
        private static ServerConfiguration _serverConfiguration;
        private static readonly int _clientInfoWaitTime = 4000;
        private static Server _server;
        private static Server _authServer;
        private static Game _currentGame;
        private static Logger? _logger;
        
        private static List<Client> _waitedClients = new();
        public static bool IsEnabled { get; private set; }
        public static Logger Logger => _logger;

        public static Game CurrentGame => _currentGame;
        public static int ServerDefaultTick => _serverConfiguration.Tick;

        public static ServerConfiguration ServerConfiguration => _serverConfiguration;
        public static ServerManager ServerManager { get; private set; }

        private Undefined()
        {
            if (_instance is not null)
                throw new ServerException("UndefinedServer is already instanced");
            _instance = this;
            EventManager.RegisterEvents(this);
        }
        
        

        private static void ShowTypeCountInfo()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly.GetName().Name is "UndefinedNetworking" or "Networking" or "Utils" or "UECS")
                    Logger.Info(assembly.GetName().Name + "    types count: " + assembly.GetTypes().Length);
            }
        }

        private static void Main()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if(Console.ReadLine() is not { } st) continue;
                    var split = st.Split(' ');
                    var prefix = split[0];
                    if(!CommandManager.TryGetCommand(prefix, out var command))return;
                    //TODO: execute command
                }
            }).Start();
            StartupServer(new MainServerLogger());
        }

        private static void Login()
        {
            /*_authServer = new Server();
            _authServer.Connect();*/
        }


        public static void Stop()
        {
            if(!IsEnabled) throw new ServerException("server is not started");
            Plugin.DisableAll();
            _currentGame.Stop();
            RuntimePacketer.IsSenderWorking = false;
            RuntimePacketer.IsThreadPoolWorking = false;
            EventManager.UnregisterEvents(_instance!);
            try
            {
                _server.Close();
            }
            catch
            {
                // ignored
            }
            _instance = null;
            IsEnabled = false;
            _logger = null;
            AppDomain.CurrentDomain.ProcessExit -= OnExit;
            EventManager.CallEvent(new ServerClosedEvent());
        }
        
        public static void StartupServer(Logger logger)
        {
            if (IsEnabled) throw new ServerException("server is already started");
            AppDomain.CurrentDomain.ProcessExit += OnExit;
            _ = new Undefined();
            ServerManager = new ServerManager();
            _ = new ChatManager();
            _ = new CommandManager();
            _logger = logger;
            LoadAll();
            _server = new Server();
            IPAddress address;
            try
            {
                address = _serverConfiguration.IPAddress;
            }
            catch (FormatException)
            {
                throw new ServerConfigurationException("invalid IP address");
            }
            _server.OpenServer(address, _serverConfiguration.Port);
            Logger.Info($"Server opened on {address.MapToIPv4()}:{_serverConfiguration.Port}");
            RuntimePacketer.IsSenderWorking = true;
            RuntimePacketer.IsThreadPoolWorking = true;
            IsEnabled = true;
            _currentGame = new Game(new GameWorld("world", 1), _logger);
            var operation = new AsyncOperationInfo<string>(10);
            Plugin.LoadAllPlugins(operation);
            operation.Wait(s => Logger.Info(s));
        }
        private static void LoadAll()
        {
            Logger.Info("Loading assemblies...");
            AppDomain.CurrentDomain.Load("Utils");
            AppDomain.CurrentDomain.Load("UECS");
            AppDomain.CurrentDomain.Load("Networking");
            AppDomain.CurrentDomain.Load("UndefinedNetworking");
            if(!ServerData.IsLoaded) ServerManager.ResourcesManager.LoadAll();
            //ShowTypeCountInfo();
            NetworkData.LoadNetworkData();
            Logger.Info("Loading configurations...");
            if (Configuration.LoadConfiguration<ServerConfiguration>() is not { } configuration)
            {
                configuration = new ServerConfiguration
                {
                    Tick = 10,
                    Port = 2402,
                    IP = "127.0.0.1",
                    IsDebugEnabled = false,
                    MaxPlayerPing = 500,
                    PingCheckDelay = 1000,
                    MaxPingInvalidRequests = 3
                };
                configuration.Save();
            }
            _serverConfiguration = configuration;
        }
        [EventHandler]
        private void OnPacketReceive(PacketReceiveEvent e)
        {
            if (e.Packet is not ClientInfoPacket packet) return;
            if (_waitedClients.Contains(e.Client))
            {
                _waitedClients.Remove(e.Client);
                if (packet.Version != ServerManager.ServerVersion)
                {
                    e.Client.Disconnect(DisconnectCause.InvalidVersion, "invalid version");
                    return;
                }
                e.Client.SendPacket(new ServerInfoPacket(_serverConfiguration.Tick, e.Client.Identifier, ChatManager.Chats, CommandManager.Commands));
                _currentGame.ConnectPlayer(e.Client, packet);
            }
            else e.Client.Disconnect(DisconnectCause.InvalidPacket, "You already connected");
        }
        
        [EventHandler]
        private void OnClientConnected(ClientConnectEvent e)
        {
            var client = new Client(e.Client, new Identifier());
            _waitedClients.Add(client);
            Task.Delay(_clientInfoWaitTime).ContinueWith(_ =>
            {
                if (_waitedClients.Contains(client))
                {
                    _waitedClients.Remove(client);
                    client.Disconnect(DisconnectCause.TimeOut, "Time out");
                }
            });
        }
        private static void OnExit(object? sender, EventArgs eventArgs)
        {
            Stop();
        }
    }
}