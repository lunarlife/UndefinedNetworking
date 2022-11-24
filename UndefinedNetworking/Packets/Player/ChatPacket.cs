using Networking;
using Networking.Packets;
using Utils;

namespace UndefinedNetworking.Packets.Player
{
    public sealed class ChatPacket : Packet
    {
        public string Nickname { get; private set; } 
        public Identifier Identifier { get; private set; }  
        public string Message { get; private set; }
        public int ChatTypeID { get; private set; }
        public Color? Color { get; private set; }
 

        public ChatPacket(Identifier identifier, string title, string message, int chatTypeId, Color color)
        {
            Message = message;
            ChatTypeID = chatTypeId;
            Color = color;
            Identifier = identifier;
            Nickname = title;
        }

        private ChatPacket()
        {
        }
    }
}