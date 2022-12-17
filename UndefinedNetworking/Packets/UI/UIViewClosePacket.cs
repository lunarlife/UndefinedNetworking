using Networking;
using Networking.Packets;

namespace UndefinedNetworking.Packets.UI;

public class UIViewClosePacket : Packet
{
    
    public Identifier Identifier { get; private set; }

    public UIViewClosePacket(Identifier identifier)
    {
        Identifier = identifier;
    }
}