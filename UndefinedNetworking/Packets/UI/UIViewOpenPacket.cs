using Networking;
using Networking.Packets;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.GameEngine.UI.Components;

namespace UndefinedNetworking.Packets.UI;

public class UIViewOpenPacket : Packet
{
    public UINetworkComponent[] Components { get; private set; }
    public ViewParameters Parameters { get; private set; }
    public Identifier Identifier { get; private set; }

    public UIViewOpenPacket(UINetworkComponent[] components, ViewParameters parameters, Identifier identifier)
    {
        Components = components;
        Parameters = parameters;
        Identifier = identifier;
    }
    private UIViewOpenPacket()
    {
    }
}