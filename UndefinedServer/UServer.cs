using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Networking;
using Networking.Events;
using Networking.Loggers;
using UndefinedNetworking;
using UndefinedNetworking.Packets.Server;
using UndefinedServer.Chats;
using UndefinedServer.Commands;
using UndefinedServer.Events;
using UndefinedServer.Exeptions;
using UndefinedServer.Gameplay;
using UndefinedServer.Loggers;
using UndefinedServer.Plugins;
using Utils;
using Utils.AsyncOperations;
using Utils.Events;
using Version = Utils.Version;

namespace UndefinedServer
{
    public class UServer
    {
        private static UServer? _instance;
        private static ServerConfigurationFile _serverConfiguration;
        private static readonly int _clientInfoWaitTime = 1000;
        private static readonly Version _version = new("0.1alpha");
        
        private static Server _server;
        private static Server _authServer;
        private static Game _currentGame;
        private static Logger? _logger;
        
        private static List<Client> _waitedClients = new();
        public static bool IsEnabled { get; private set; }
        public static Logger Logger => _logger;

        public static Game CurrentGame => _currentGame;

        public UServer()
        {
            if (_instance is not null)
                throw new ServerException("UndefinedServer is already instanced");
            _instance = this;
            EventManager.RegisterEvents(this);
        }
        
        private static void LoadAll()
        {
            Logger.Info("Loading assemblies...");
            AppDomain.CurrentDomain.Load("UndefinedNetworking");
            AppDomain.CurrentDomain.Load("Utils");
            Logger.Info("Loading configurations...");
            if (Configuration.LoadConfiguration<ServerConfigurationFile>() is not { } configuration)
            {
                configuration = new ServerConfigurationFile
                {
                    Tick = 10,
                    Port = 2402,
                    IP = "127.0.0.1"
                };
                configuration.Save();
            }
            _serverConfiguration = configuration;
        }
        private static void Main()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if(Console.ReadLine() is not { } st) continue;
                    if (st == "stop")
                    {
                        Environment.Exit(0);
                    }
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
            _ = new UServer();
            _ = new ChatManager();
            _ = new CommandManager();
            _logger = logger;
            if(!ServerData.IsLoaded) ServerData.LoadData();
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
            RuntimePacketer.IsSenderWorking = true;
            RuntimePacketer.IsThreadPoolWorking = true;
            _currentGame = new Game(new World("world", 1), _logger);
            var operation = new AsyncOperationInfo<string>(60);
            Plugin.LoadAllPlugins(operation);
            operation.Wait(s => Logger.Info(s));
            IsEnabled = true;
        }

        [EventHandler]
        private void OnPacketReceive(PacketReceiveEvent e)
        {
            if (e.Packet is not ClientInfoPacket packet) return;
            if (_waitedClients.Contains(e.Client))
            {
                _waitedClients.Remove(e.Client);
                if (packet.Version != _version)
                {
                    e.Client.Disconnect(DisconnectCause.InvalidVersion, "you has invalid version");
                    return;
                }
                e.Client.SendPacket(new ServerInfoPacket(_serverConfiguration.Tick, e.Client.Identifier, ChatManager.Chats, CommandManager.Commands));
                _currentGame.ConnectPlayer(new ServerPlayer(e.Client, packet.Name));
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