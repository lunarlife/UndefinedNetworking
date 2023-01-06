using Networking.Packets;

namespace UndefinedNetworking.Packets.UI;

public class UIViewClosePacket : Packet
{
    
    public uint Identifier { get; private set; }

    public UIViewClosePacket(uint identifier)
    {
        Identifier = identifier;
    }
}