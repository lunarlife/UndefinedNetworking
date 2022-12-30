using Networking;
using UndefinedNetworking;
using UndefinedNetworking.Chats;
using UndefinedNetworking.Events.UIEvents;
using UndefinedNetworking.GameEngine.Scenes;
using UndefinedNetworking.Gameplay;
using UndefinedNetworking.Packets.Components;
using UndefinedNetworking.Packets.Player;
using UndefinedNetworking.Packets.UI;
using UndefinedNetworking.Packets.World;
using UndefinedServer.Events;
using UndefinedServer.Events.PlayerEvents;
using UndefinedServer.GameEngine.Scenes;
using UndefinedServer.Pings;
using Utils.Events;

namespace UndefinedServer
{
    public class Player : IPlayer, IEventCaller<PlayerDisconnectedEvent>
    {
        private readonly Client _client;
        private Game? _currentGame;
        private bool _isOnline;
        public IScene ActiveScene { get; private set; }
        public Identifier Identifier => _client.Identifier;
        public string Nickname { get; }
        public string SenderName => Nickname;
        public bool IsOnline => _isOnline;
        public Ping NetworkPing { get; }
        public Ping TotalPing { get; }
        public Game? CurrentGame
        {
            get => _currentGame;
            set
            {
                _currentGame = value;
                if(_currentGame is not null)
                    _client.SendPacket(new WorldPacket(_currentGame.World.Seed));
            }
        }
        internal Client Client => _client;
        
        internal Player(Client client, string nickname)
        {
            NetworkPing = new NetworkPing(this);
            TotalPing = new TotalPing(this);
            Nickname = nickname;
            _client = client;
            _client.OnDisconnect += OnClientDisconnect;
            EventManager.RegisterEvent(client, OnPacketReceive);
            _isOnline = true;
        }

        private void OnPacketReceive(PacketReceiveEvent e)
        {
            switch (e.Packet)
            {
                case UIComponentUpdatePacket packet:
                    break;
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