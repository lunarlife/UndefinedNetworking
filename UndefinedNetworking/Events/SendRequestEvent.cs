using Networking.Packets;
using Utils.Events;

namespace UndefinedNetworking.Events
{
    public class SendRequestEvent : Event
    {
        public Packet Request { get; }
        public SendRequestEvent(Packet request)
        {
            Request = request;
        }

    }
}