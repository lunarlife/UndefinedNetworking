using Networking.Packets;
using Utils.Events;

namespace UndefinedServer.Events
{
    public class PacketReceiveEvent : Event
    {
        public Packet Packet { get; }
        public Client Client { get; }

        public PacketReceiveEvent(Packet packet, Client client)
        {
            Packet = packet;
            Client = client;
        }
    }
}