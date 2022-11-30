using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Networking;
using Networking.Loggers;
using Networking.Packets;
using UECS;
using UndefinedNetworking;
using UndefinedNetworking.Chats;
using UndefinedNetworking.Packets.Player;
using UndefinedServer.Chats;
using UndefinedServer.Events;
using UndefinedServer.Events.PlayerEvents;
using UndefinedServer.Gameplay;
using UndefinedServer.UI;
using Utils;
using Utils.Events;

namespace UndefinedServer
{
    public sealed class Game : IEventCaller<UpdateEvent>
    {
        private Thread? _updateThread; 
        private readonly Logger _logger;
        private readonly SystemsController _systems = new();
        private readonly Dictionary<Identifier, Player> _players = new();
        public IEnumerable<Player> Players => _players.Values;
        
        public World World { get; }

        internal Game(World world, Logger logger)
        {
            _logger = logger;
            World = world;
            _systems.Add(new NetComponentSystem());
            StartUpdateLoop();
            this.RegisterListener();
        }

        private void StartUpdateLoop()
        {
            _updateThread = new Thread(() =>
            {
                while (Undefined.IsEnabled)
                {
                    Thread.Sleep(Undefined.ServerDefaultTick);
                    Update();
                }
            })
            {
                Name = "Update loop"
            };
            _updateThread.Start();
        } 
        private void Update()
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
            _systems.Update();
            this.CallEvent(new UpdateEvent());
        }
        [EventHandler]
        private void OnPacketReceive(PacketReceiveEvent e)
        {
            var identifier = e.Client.Identifier;
            if(!_players.ContainsKey(identifier)) return;
            switch (e.Packet)
            {
                case PlayerDisconnectPacket pdp:
                {
                    var player = _players[identifier];
                    SendGamePacket(player, new PlayerDisconnectPacket(identifier, pdp.Cause, pdp.Message));
                    e.Client.Disconnect(pdp.Cause, pdp.Message);
                    break;
                }
            }
        }

        [EventHandler]
        private void OnPlayerDisconnected(PlayerDisconnectedEvent e)
        {
            var player = e.Player;
            _players.Remove(player.Identifier);
            _logger.Info($"Player {player.Nickname} with id {player.Identifier}: {e.Message}");
        }
        public void SaveAll()
        {
            //TODO: do it
        }
        internal void ConnectPlayer(Player player)
        {
            player.CurrentGame = this;
            _players.Add(player.Identifier, player);
            _logger.Info($"Player {player.Nickname} with id {player.Identifier} joined");
            SendGamePacket(player, new PlayerConnectPacket(player.Identifier, player.Nickname));
            EventManager.CallEvent(new PlayerConnectedEvent(player));
            
            
            if(ChatManager.DebugChatIsEnabled) player.SendMessage(new ChatMessage(ServerSender.Instance, "sosi hui ebalai", Color.DarkRed, ChatManager.GetChat("debug")));
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