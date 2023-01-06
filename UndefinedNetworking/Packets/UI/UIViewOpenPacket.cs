using Networking.Packets;

namespace UndefinedNetworking.Packets.UI;

public class UIViewOpenPacket : Packet
{
    public uint Identifier { get; private set; }

    public UIViewOpenPacket(uint identifier)
    {
        Identifier = identifier;
    }
}