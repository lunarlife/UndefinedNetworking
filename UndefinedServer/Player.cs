using System;
using System.Collections.Generic;
using Networking;
using UndefinedNetworking;
using UndefinedNetworking.Chats;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.Gameplay;
using UndefinedNetworking.Packets.Player;
using UndefinedNetworking.Packets.World;
using UndefinedServer.UI.View;

namespace UndefinedServer
{
    public class Player : IPlayer
    {
        private readonly Client _client;
        private Game? _currentGame;
        private readonly List<UIView> _elements = new();
        public Identifier Identifier => _client.Identifier;
        public string Nickname { get; }
        public string SenderName => Nickname;
        public IEnumerable<IUIView> ViewElements => _elements;


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
            Nickname = nickname;
            _client = client;
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