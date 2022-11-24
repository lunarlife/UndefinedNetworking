using Networking.Packets;
using Utils.Events;

namespace UndefinedServer.Events
{
    public class PacketReceiveEvent : Event
    {
        public Packet Packet { get; }
        internal Client Client { get; }

        internal PacketReceiveEvent(Packet packet, Client client)
        {
            Packet = packet;
            Client = client;
        }
    }
}