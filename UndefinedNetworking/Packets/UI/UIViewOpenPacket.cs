using Networking;
using Networking.Packets;

namespace UndefinedNetworking.Packets.UI;

public class UIViewOpenPacket : Packet
{
    public Identifier Identifier { get; private set; }

    public UIViewOpenPacket(Identifier identifier)
    {
        Identifier = identifier;
    }
}