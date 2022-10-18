using Networking;
using Networking.DataConvert;
using Networking.Packets;
using Utils;

namespace UndefinedNetworking.Packets.Player
{
    [DataObject]
    public sealed class ChatPacket : Packet
    {
        [DataProperty]
        public string Nickname { get; private set; } 
        [DataProperty]
        public Identifier Identifier { get; private set; }  
        [DataProperty]
        public string Message { get; private set; }
        [DataProperty]
        public int ChatTypeID { get; private set; }
        [DataProperty]
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