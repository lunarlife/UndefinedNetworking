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
using UndefinedServer.Exceptions;
using UndefinedServer.GameEngine;
using UndefinedServer.Gameplay;
using UndefinedServer.Loggers;
using UndefinedServer.Plugins;
using Utils;
using Utils.Events;

namespace UndefinedServer
{
    public sealed class Undefined : IEventListener
    {
        private static Undefined? _instance;
        private static ServerConfiguration _serverConfiguration;
        private static readonly int _clientInfoWaitTime = 4000;
        private static Server _server;
        private static Server _authServer;
        private static Game _currentGame;
        private static Logger _logger;
        
        private static List<Client> _waitedClients = new();
        public static bool IsEnabled { get; private set; }
        public static Logger Logger => ServerManager.Logger;

        public static Game CurrentGame => _currentGame;
        public static int ServerDefaultTick => _serverConfiguration.Tick;

        public static ServerConfiguration ServerConfiguration => _serverConfiguration;
        public static ServerManager ServerManager { get; private set; }
        public static Event<ServerClosedEventData> ServerClose { get; } = new();
        private Undefined(Logger logger)
        {
            _instance = _instance is null ? this : throw new ServerException("Sever is already instanced");
            if (IsEnabled) throw new ServerException("server is already started");
            AppDomain.CurrentDomain.ProcessExit += OnExit;
            _logger = logger;
            ServerManager = new ServerManager(_logger);
            Initialize();
        }

        private async void Initialize()
        {
            _ = new ChatManager();
            _ = new CommandManager();
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
            await Plugin.LoadAllPlugins(); 
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
            ServerClose.Invoke(new ServerClosedEventData());
        }
        
        public static void StartupServer(Logger logger)
        {
            _ = new Undefined(logger);
           
        }
        private static void LoadAll()
        {
            if(!ServerData.IsLoaded) ServerManager.ResourcesManager.LoadAll();
            //ShowTypeCountInfo();
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
        private void OnClientInfoReceive(ClientInfoPacket packet, Client client)
        {
            if (_waitedClients.Contains(client))
            {
                _waitedClients.Remove(client);
                if (packet.Version != ServerManager.ServerVersion)
                {
                    client.Disconnect(DisconnectCause.InvalidVersion, "invalid version");
                    return;
                }
                client.SendPacket(new ServerInfoPacket(_serverConfiguration.Tick, client.Identifier, ChatManager.Chats, CommandManager.Commands));
                _currentGame.ConnectPlayer(client, packet);
            }
            else client.Disconnect(DisconnectCause.InvalidPacket, "You already connected");
        }
        
        [EventHandler]
        private void OnClientConnected(ClientConnectEventData e)
        {
            var client = new Client(e.Client, new Identifier());
            client.OnPacketReceive.AddListener(data =>
            {
                if(data.Packet is ClientInfoPacket packet)
                    OnClientInfoReceive(packet, client);
            });
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