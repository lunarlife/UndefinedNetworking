using System.Collections.Generic;
using System.Linq;
using Networking;
using Networking.DataConvert;
using UndefinedNetworking;
using UndefinedNetworking.Chats;
using UndefinedNetworking.Exceptions;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.GameEngine.UI.Components;
using UndefinedNetworking.Gameplay;
using UndefinedNetworking.Packets.Player;
using UndefinedNetworking.Packets.UI;
using UndefinedNetworking.Packets.World;
using UndefinedServer.Events.PlayerEvents;
using UndefinedServer.Pings;
using UndefinedServer.UI.View;
using Utils.Events;

namespace UndefinedServer
{
    public class Player : IPlayer, IEventCaller<PlayerDisconnectedEvent>
    {
        private readonly Client _client;
        private Game? _currentGame;
        private readonly List<IUIView> _elements = new();
        private bool _isOnline;
        public Identifier Identifier => _client.Identifier;
        public string Nickname { get; }
        public string SenderName => Nickname;
        public IEnumerable<IUIView> ViewElements => _elements;

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
            _isOnline = true;
            foreach (var view in _elements) view.Close();
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
        public IUIView Open(IUIElement element)
        {
            var view = new UIView(element, this, null);
            var transform = view.Transform;
            var packet = new UIViewOpenPacket(view.Components.Where(c => c is UINetworkComponent).Cast<UINetworkComponent>().ToArray(), new ViewParameters
            {
                Bind = transform.Bind,
                Layer = transform.Layer,
                Margins = transform.Margins,
                Pivot = transform.Pivot,
                IsActive = transform.IsActive,
                OriginalRect = transform.OriginalRect
            }, view.Identifier);
            _elements.Add(view);
            _client.SendPacket(packet);
            return view;
        }

        public void Close(IUIView view)
        {
            if (!_elements.Contains(view))
                throw new ViewException("current view not opened on player");
            _elements.Remove(view);
            if(_isOnline)
                _client.SendPacket(new UIViewClosePacket(view.Identifier));
        }
    }
}