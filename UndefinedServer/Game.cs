using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Networking;
using Networking.Loggers;
using Networking.Packets;
using UndefinedNetworking;
using UndefinedNetworking.Chats;
using UndefinedNetworking.Packets.Player;
using UndefinedServer.Chats;
using UndefinedServer.Events;
using UndefinedServer.Events.Player;
using UndefinedServer.Gameplay;
using Utils;
using Utils.Events;

namespace UndefinedServer
{
    public sealed class Game
    {
        private readonly Logger _logger;
        private Dictionary<Identifier, ServerPlayer> _players = new();
        public IEnumerable<ServerPlayer> Players => _players.Values;

        public World World { get; }

        internal Game(World world, Logger logger)
        {
            _logger = logger;
            World = world;
            this.RegisterListener();
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
                    _players.Remove(identifier);
                    _logger.Info($"Player {player.Nickname} with id {identifier}: {pdp.Message}");
                    break;
                }
            }
        }
        
        public void SaveAll()
        {
            //TODO: do it
        }
        public void ConnectPlayer(ServerPlayer serverPlayer)
        {
            serverPlayer.CurrentGame = this;
            _players.Add(serverPlayer.Identifier, serverPlayer);
            _logger.Info($"Player {serverPlayer.Nickname} with id {serverPlayer.Identifier} joined");
            SendGamePacket(serverPlayer, new PlayerConnectPacket(serverPlayer.Identifier, serverPlayer.Nickname));
            EventManager.CallEvent(new PlayerConnectedEvent(serverPlayer));
            
            
            if(ChatManager.DebugChatIsEnabled) serverPlayer.SendMessage(new ChatMessage(ServerSender.Instance, "sosi hui ebalai", Color.DarkRed, ChatManager.GetChat("debug")));
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
        internal void SendGamePacket(ServerPlayer? without, params Packet[] packets)
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

        public ServerPlayer GetPlayer(Identifier id)
        {
            if (!_players.ContainsKey(id))
                throw new GameException("player not founded");
            return _players[id];
        }
        public ServerPlayer GetPlayer(string nickname) => _players.Values.FirstOrDefault(player => player.Nickname == nickname) ?? throw new GameException("player not founded");

        public bool TryGetPlayer(Identifier id, out ServerPlayer? player)
        {
            if (!_players.ContainsKey(id))
            {
                player = null;
                return false;
            }
            player = _players[id];
            return true;
        }
        public bool TryGetPlayer(string nickname, out ServerPlayer? player)
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