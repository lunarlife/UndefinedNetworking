using Networking.Packets;
using Utils.Events;

namespace UndefinedNetworking.Events
{
    public class ReceiveAnswerEvent : Event
    {
        public Packet Answer { get; }
        public ReceiveAnswerEvent(Packet answer)
        {
            Answer = answer;
        }

    }
}