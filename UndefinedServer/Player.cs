using System;
using System.Collections.Generic;
using Networking;
using UndefinedNetworking;
using UndefinedNetworking.Chats;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.Gameplay;
using UndefinedNetworking.Packets.Player;
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
        private readonly List<UIView> _elements = new();
        public Identifier Identifier => _client.Identifier;
        public string Nickname { get; }
        public string SenderName => Nickname;
        public IEnumerable<IUIView> ViewElements => _elements;
        
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

        }

        private void OnClientDisconnect(DisconnectCause cause, string message)
        {
            this.CallEvent(new PlayerDisconnectedEvent(this, cause, message));
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
           // var netComponents = view.Components.Select(component => Component.ToNetComponent(component, view)).ToArray();
            //_client.SendPacket(new UIViewOpenPacket(parameters, ));
            return null;
        }

        public void Close(IUIView view)
        {
            throw new NotImplementedException();
        }
        
        void IUIViewer.OnUpdateView(IUIView view)
        {
            
        }
    }
}