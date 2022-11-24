using Networking.Packets;

namespace UndefinedNetworking.Packets.Command
{
    public sealed class CommandPacket : Packet
    {
        public int CommandId { get; private set; }
        public int ChatId { get; private set; }
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