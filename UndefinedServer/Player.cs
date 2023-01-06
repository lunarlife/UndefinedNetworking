using System.IO;
using Networking;
using UndefinedNetworking;
using UndefinedNetworking.Chats;
using UndefinedNetworking.Events.UIEvents;
using UndefinedNetworking.GameEngine.Scenes;
using UndefinedNetworking.Gameplay;
using UndefinedNetworking.Packets.Components;
using UndefinedNetworking.Packets.Player;
using UndefinedNetworking.Packets.Server.Resources;
using UndefinedNetworking.Packets.UI;
using UndefinedNetworking.Packets.World;
using UndefinedServer.Events;
using UndefinedServer.Events.PlayerEvents;
using UndefinedServer.GameEngine;
using UndefinedServer.GameEngine.Scenes;
using UndefinedServer.Pings;
using Utils.Events;

namespace UndefinedServer
{
    public class Player : IPlayer, IEventCaller<PlayerConnectedEvent>, IEventCaller<PlayerDisconnectedEvent>
    {
        private readonly Client _client;
        private Game? _currentGame;
        
        private bool _isOnline;
        private PlayerConnectionState _state;
        private int _downloadedRes = 0;
        public IScene ActiveScene { get; private set; }
        public Identifier Identifier => _client.Identifier;
        public string Nickname { get; }
        public string SenderName => Nickname;
        public bool IsOnline => _isOnline;
        public Ping NetworkPing { get; }
        public Ping TotalPing { get; }
        public Game? CurrentGame => _currentGame;
        internal Client Client => _client;

        public int DownloadedResourcesCount => _downloadedRes;

        public bool IsConnectedAndReady => IsOnline && _state == PlayerConnectionState.ConnectedAndReady;
        public PlayerConnectionState State => _state;

        internal Player(Client client, string nickname, Game game)
        {
            _state = PlayerConnectionState.Connecting;
            NetworkPing = new NetworkPing(this);
            TotalPing = new TotalPing(this);
            Nickname = nickname;
            _client = client;
            _client.OnDisconnect += OnClientDisconnect;
            EventManager.RegisterEvent(client, OnPacketReceive);
            _currentGame = game;
            _client.SendPacket(new WorldPacket(_currentGame.World.Seed));
            _isOnline = true;
            SendNextResource();
        }
        
        private void OnPacketReceive(PacketReceiveEvent e)
        {
            switch (e.Packet)
            {
                case UIComponentUpdatePacket packet:
                    break;
            }
        }

        private void SendNextResource()
        {
            if (Undefined.ServerManager.ResourcesManager.ResourcesCount == _downloadedRes)
            {
                _state = PlayerConnectionState.ConnectedAndReady;
                this.CallEvent(new PlayerConnectedEvent(this));
                return;
            }
            var res = Undefined.ServerManager.ResourcesManager.GetResource(_downloadedRes);
            using var stream = File.OpenRead(res.FullPath);
            var length = stream.Length;
            _client.Request(new ResourceDownloadStartPacket(res.Path, _downloadedRes, length), _ =>
            {
                _downloadedRes++;
                SendNextResource();
            });
            const long packetLength = 8192;
            while (length != 0)
            {
                var buffLength = length < packetLength ? length : packetLength;
                length -= buffLength;
                var buffer = new byte[buffLength];
                _ = stream.Read(buffer);
                _client.SendPacket(new ResourcesPacket(buffer));
            }
        }
        public void LoadScene(SceneType type)
        {
            ActiveScene = type is SceneType.XY ? new Scene2D(this) : new Scene3D(this);
            EventManager.RegisterEvent<UICloseEvent>(ActiveScene, OnUIClose);
            EventManager.RegisterEvent<UIOpenEvent>(ActiveScene, OnOpenView);
        }
        private void OnClientDisconnect(DisconnectCause cause, string message)
        {
            this.CallEvent(new PlayerDisconnectedEvent(this, cause, message));
            _isOnline = false;
            NetworkPing.Dispose();
            TotalPing.Dispose();
        }

        public void Kick(string message)
        {
            _client.Disconnect(DisconnectCause.Kicked, message);
        }
        
        public void SendMessage(ChatMessage message)
        {
            _client.SendPacket(new ChatPacket(_client.Identifier, message.Title, message.Text, message.Chat, message.Color));
        }
        
        
        private void OnOpenView(UIOpenEvent e)
        {
            var view = e.View;
            var packet = new UIViewOpenPacket(view.Identifier);
            if(_isOnline)
                _client.SendPacket(packet);
        }

        public void OnUIClose(UICloseEvent e)
        {
            var view = e.View;
            if(_isOnline)
                _client.SendPacket(new UIViewClosePacket(view.Identifier));
        }
    }
}