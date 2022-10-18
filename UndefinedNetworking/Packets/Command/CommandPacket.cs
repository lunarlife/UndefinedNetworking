using Networking.DataConvert;
using Networking.Packets;

namespace UndefinedNetworking.Packets.Command
{
    [DataObject]
    public sealed class CommandPacket : Packet
    {
        [DataProperty]
        public int CommandId { get; private set; }
        [DataProperty]
        public int ChatId { get; private set; }
        [DataProperty]
        public string[]? Parameters { get; private set; }

        public CommandPacket(int commandId, string[] parameters, int chatId)
        {
            CommandId = commandId;
            ChatId = chatId;
            Parameters = parameters;
        }

        private CommandPacket()
        {
            
        }
    }
}