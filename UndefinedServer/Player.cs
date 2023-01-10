using System.IO;
using Networking;
using Networking.Packets;
using UndefinedNetworking;
using UndefinedNetworking.Chats;
using UndefinedNetworking.Events.UIEvents;
using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Scenes;
using UndefinedNetworking.GameEngine.Scenes.UI.Components;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;
using UndefinedNetworking.Gameplay;
using UndefinedNetworking.Packets.Components;
using UndefinedNetworking.Packets.Player;
using UndefinedNetworking.Packets.Server.Resources;
using UndefinedNetworking.Packets.UI;
using UndefinedNetworking.Packets.World;
using UndefinedServer.Events;
using UndefinedServer.Events.PlayerEvents;
using UndefinedServer.Exceptions;
using UndefinedServer.GameEngine.Scenes;
using UndefinedServer.Pings;
using Utils.Events;

namespace UndefinedServer
{
    public class Player : IPlayer, IEventListener
    {
        private readonly Client _client;
        private Game? _currentGame;
        
        private bool _isOnline;
        private PlayerConnectionState _state;
        private int _downloadedRes = 0;
        private bool _isDownloadingRes;
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
        public Event<PlayerResourcesDownloadedEventData> OnResourcesDownloaded { get; } = new();
        public Event<PlayerDisconnectedEventData> OnDisconnect { get; } = new();

        internal Player(Client client, string nickname, Game game)
        {
            _state = PlayerConnectionState.Connecting;
            NetworkPing = new NetworkPing(this);
            TotalPing = new TotalPing(this);
            Nickname = nickname;
            _client = client;
            _client.OnDisconnect += OnClientDisconnect;
            client.OnPacketReceive.AddListener(OnPacketReceive);
            _currentGame = game;
            _client.SendPacket(new WorldPacket(_currentGame.World.Seed));
            EventManager.RegisterEvents(this);
            _isOnline = true;
        }
        
        private void OnPacketReceive(PacketReceiveEventData e)
        {
            switch (e.Packet)
            {
                case UIComponentUpdatePacket packet:
                    break;
                case PlayerDisconnectPacket pdp:
                    Disconnect(pdp.Cause, pdp.Message);
                    break;
            }
        }


        private void Disconnect(DisconnectCause cause, string message)
        {
            DisconnectLocal();
            Client.Disconnect(cause, message);
            OnDisconnect.Invoke(new PlayerDisconnectedEventData(this, cause, message));
        }
        private void DisconnectLocal()
        {
            _isOnline = false;
            NetworkPing.Dispose();
            TotalPing.Dispose();
            ActiveScene.Unload();
        }
        [EventHandler]
        private void OnComponentUpdate(ComponentRemoteUpdateEventData e)
        {
            if(((IUIViewBase)e.Component.TargetObject).ContainsViewer(this))
                Client.SendPacket(new UIComponentUpdatePacket((IComponent<UINetworkComponentData>)e.Component));
        }

        public void UpdatePlayerResources()
        {
            if (_isDownloadingRes) throw new ResourceDownloadException("resource is already downloading");
            _downloadedRes = 0;
            SendNextResource();
        }
        private void SendNextResource()
        {
            if (Undefined.ServerManager.ResourcesManager.ResourcesCount == _downloadedRes)
            {
                _state = PlayerConnectionState.ConnectedAndReady;
                _isDownloadingRes = false;
                OnResourcesDownloaded.Invoke(new PlayerResourcesDownloadedEventData(this));
                return;
            }

            _isDownloadingRes = true;
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
            ActiveScene.UIOpen.AddListener(OnOpenView);
        }
        private void OnClientDisconnect(DisconnectCause cause, string message)
        {
            DisconnectLocal();
        }

        public void Kick(string message)
        {
            Disconnect(DisconnectCause.Kicked, message);
        }
        
        public void SendMessage(ChatMessage message)
        {
            _client.SendPacket(new ChatPacket(_client.Identifier, message.Title, message.Text, message.Chat, message.Color));
        }
        
        
        private void OnOpenView(UIOpenEventData e)
        {
            e.View.OnClose.AddListener(OnUIClose);
            var view = e.View;
            var packet = new UIViewOpenPacket(view.Identifier);
            if(_isOnline)
                _client.SendPacket(packet);
        }

        private void OnUIClose(UICloseEventData e)
        {
            var view = e.View;
            if(_isOnline)
                _client.SendPacket(new UIViewClosePacket(view.Identifier));
        }
    }
}