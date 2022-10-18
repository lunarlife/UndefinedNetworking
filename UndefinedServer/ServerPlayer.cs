using Networking;
using UndefinedNetworking;
using UndefinedNetworking.Chats;
using UndefinedNetworking.Gameplay;
using UndefinedNetworking.Packets.Player;
using UndefinedNetworking.Packets.World;

namespace UndefinedServer
{
    public class ServerPlayer : IPlayer
    {
        public string Name => Nickname;
        public string Nickname { get; }

        private readonly Client _client;
        private Game? _currentGame;
        
        public Identifier Identifier => _client.Identifier;
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
        public ServerPlayer(Client client, string nickname)
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
    }
}