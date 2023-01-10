using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Networking;
using Networking.Loggers;
using Networking.Packets;
using UECS;
using UndefinedNetworking;
using UndefinedNetworking.Events.GameEngine;
using UndefinedNetworking.Packets.Player;
using UndefinedNetworking.Packets.Server;
using UndefinedNetworking.Packets.Server.Resources;
using UndefinedServer.Events.PlayerEvents;
using UndefinedServer.Gameplay;
using UndefinedServer.UI;
using Utils.Events;

namespace UndefinedServer
{
    public sealed class Game : IEventListener
    {
        private Thread? _tickThread;
        private DateTime _lastTickTime;
        private readonly Logger _logger;
        private readonly SystemsController _systems = new();
        private readonly Dictionary<Identifier, Player> _players = new();
        public IEnumerable<Player> Players => _players.Values;
        
        public GameWorld World { get; }

        public SystemsController Systems => _systems;
        public Event<TickEventData> Tick { get; } = new();
        public Event<PlayerPreConnectingEventDataData> PlayerPreConnecting { get; } = new();
        public Event<PlayerConnectedEventData> PlayerConnected { get; } = new();

        internal Game(GameWorld world, Logger logger)
        {
            _logger = logger;
            World = world;
            var netSystem = new NetComponentSystem();
            _systems.Register(netSystem);
            _systems.Register(new MouseHandlersSystem());
            StartUpdateLoop();
            EventManager.RegisterEvents(this);
        }

        private void StartUpdateLoop()
        {
            _tickThread = new Thread(() =>
            {
                _lastTickTime = DateTime.Now;
                while (Undefined.IsEnabled)
                {
                    Thread.Sleep(Undefined.ServerDefaultTick);
                    DoTick();
                }
            })
            {
                Name = "Update loop"
            };
            _tickThread.Start();
        } 
        private void DoTick()
        {
            
            foreach (var player in from player in _players.Values
                     let time = DateTime.Now
                     where (time - player.NetworkPing.LastPingUpdate).TotalMilliseconds >=
                           Undefined.ServerConfiguration.PingCheckDelay
                     select player)
            {
                var networkPing = player.NetworkPing;
                var totalPing = player.TotalPing;
                if (networkPing.InvalidRequestsCount >= Undefined.ServerConfiguration.MaxPingInvalidRequests || totalPing.InvalidRequestsCount >= Undefined.ServerConfiguration.MaxPingInvalidRequests)
                {
                    player.Client.Disconnect(DisconnectCause.TimeOut, "Invalid ping requests count");
                    continue;
                }
                totalPing.Update();
                networkPing.Update();
            }
            _systems.UpdateSync();
            _systems.UpdateAsync();
            var now = DateTime.Now;
            var delta = now - _lastTickTime;
            _lastTickTime = now;
            Tick.Invoke(new TickEventData((float)delta.TotalMilliseconds / 1000f));
        }



        [EventHandler]
        private void OnPlayerDisconnect(PlayerDisconnectedEventData e)
        {
            var player = e.Player;
            //SendGamePacket(player, new PlayerDisconnectPacket(e.Player.Identifier, e.Cause, e.Message));
        }

        [EventHandler]
        private void OnPlayerDisconnected(PlayerDisconnectedEventData e)
        {
            var player = e.Player;
            _players.Remove(player.Identifier);
            _logger.Info($"Player {player.Nickname} with id {player.Identifier} left the game");
        }
        public void SaveAll()
        {
            //TODO: do it
        }
        internal void ConnectPlayer(Client client, ClientInfoPacket packet)
        {
            //SendFilesFromDirectory(client, Paths.ResourcesFolder);
            var player = new Player(client, packet.Name, this);
            player.OnResourcesDownloaded.AddListener(e => PlayerConnected.Invoke(new PlayerConnectedEventData(player)));
            player.UpdatePlayerResources();
            _players.Add(player.Identifier, player);
            _logger.Info($"Player {player.Nickname} with id {player.Identifier} joined");
            PlayerPreConnecting.Invoke(new PlayerPreConnectingEventDataData(player));
//            if(ChatManager.DebugChatIsEnabled) player.SendMessage(new ChatMessage(ServerSender.Instance, "sosi hui ebalai", Color.DarkRed, ChatManager.GetChat("debug")));
        }

        private void SendFilesFromDirectory(Client client, string dir)
        {
            var packets = new List<Packet>();
            const int packetLength = 1024;
            foreach (var file in Directory.GetFiles(dir))
            {
                if(!file.EndsWith(".png")) continue;
                var fileWithoutInResourcesFolder = file.Replace(Paths.ResourcesFolder, "");
                using var stream = File.OpenRead(file);
                var read = 1;
                while (read != 0)
                {
                    var buffer = new byte[packetLength];
                    read = stream.Read(buffer);
                    packets.Add(new ResourcesPacket(read == packetLength ? buffer : buffer[..read]));
                }
            }
            client.SendPacket(packets.ToArray());
            foreach (var dir1 in Directory.GetDirectories(dir)) SendFilesFromDirectory(client, dir1);
        }
        public void Stop()
        {
            foreach (var player in _players.Values)
            {
                player.Client.Disconnect(DisconnectCause.ServerClosed,"Server closed");
            }
            World.Unload();
            while (Players.Any(p => p.Client.WaitBeforeCloseServer))
            {
                Thread.Sleep(10);
            }
        }
        internal void SendGamePacket(params Packet[] packets)
        {
            SendGamePacket(null, packets);
        }
        internal void SendGamePacket(Player? without, params Packet[] packets)
        {
            if (packets.Length == 0)
                return;
            var players = _players.Values.ToList();
            for (var i = 0; i < players.Count; i++)
            {
                var player = players[i];
                if(player != without)
                    player.Client.SendPacket(packets);
            }
        }

        public Player GetPlayer(Identifier id)
        {
            if (!_players.ContainsKey(id))
                throw new GameException("player not founded");
            return _players[id];
        }
        public Player GetPlayer(string nickname) => _players.Values.FirstOrDefault(player => player.Nickname == nickname) ?? throw new GameException("player not founded");

        public bool TryGetPlayer(Identifier id, out Player? player)
        {
            if (!_players.ContainsKey(id))
            {
                player = null;
                return false;
            }
            player = _players[id];
            return true;
        }
        public bool TryGetPlayer(string nickname, out Player? player)
        {
            if (_players.Values.FirstOrDefault(player => player.Nickname == nickname) is not { } p)
            {
                player = null;
                return false;
            }
            player = p;
            return true;
        }
    }

    public class GameException : Exception
    {
        public GameException(string msg) : base(msg)
        {
        }
    }
}