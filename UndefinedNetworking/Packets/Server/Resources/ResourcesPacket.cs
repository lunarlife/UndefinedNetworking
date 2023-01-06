using Networking.Packets;

namespace UndefinedNetworking.Packets.Server.Resources;

public class ResourcesPacket : Packet
{
    public byte[] Data { get; private set; }
    public ResourcesPacket(byte[] data)
    {
        Data = data;
    }
}