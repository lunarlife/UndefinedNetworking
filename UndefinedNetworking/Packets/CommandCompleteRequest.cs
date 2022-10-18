using Networking.DataConvert;
using Networking.Packets;

namespace UndefinedNetworking.Packets
{
    [DataObject]
    public class CommandCompleteRequest : RequestPacket<CommandCompleteAnswer>
    {
        [DataProperty]
        public int CommandId { get; private set; }

        public CommandCompleteRequest(int commandId)
        {
            CommandId = commandId;
        }
        private CommandCompleteRequest()
        {
            
        }
    }
    [DataObject]
    public class CommandCompleteAnswer : Packet
    {
        [DataProperty]
        public ParameterCompletesPacket[]? Completes { get; private set; }

        public CommandCompleteAnswer(ParameterCompletesPacket[]? completes)
        {
            Completes = completes;
        }
        private CommandCompleteAnswer()
        {
        }
    }

    [DataObject]
    public class ParameterCompletesPacket
    {
        [DataProperty]
        public string[] Parameters { get; private set; }
        [DataProperty]
        public bool IsRepeatable { get; private set; }
        [DataProperty]
        public string Title { get; private set; }

        public ParameterCompletesPacket(string[] parameters, bool isRepeatable, string title)
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