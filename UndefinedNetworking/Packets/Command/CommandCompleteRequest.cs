using Networking.Packets;

namespace UndefinedNetworking.Packets.Command
{
    public class CommandCompleteRequest : RequestPacket<CommandCompleteAnswer>
    {
        public int CommandId { get; private set; }

        public CommandCompleteRequest(int commandId)
        {
            CommandId = commandId;
        }
        private CommandCompleteRequest()
        {
            
        }
    }
    public class CommandCompleteAnswer : Packet
    {
        public ParameterCompletesPacket[]? Completes { get; private set; }

        public CommandCompleteAnswer(ParameterCompletesPacket[]? completes)
        {
            Completes = completes;
        }
        private CommandCompleteAnswer()
        {
        }
    }

    public class ParameterCompletesPacket
    {
        public string[]? Parameters { get; private set; }
        public bool IsRepeatable { get; private set; }
        public string Title { get; private set; }

        public ParameterCompletesPacket(string[]? parameters, bool isRepeatable, string title)
        {
            Parameters = parameters;
            IsRepeatable = isRepeatable;
            Title = title;
        }
        private ParameterCompletesPacket()
        {
        }
    }
}