using Networking;
using Networking.Packets;
using UndefinedNetworking.GameEngine.UI.Components;

namespace UndefinedNetworking.Packets.Components;

public class UIComponentAddPacket : Packet
{
    public Identifier ViewIdentifier { get; }
    public UINetworkComponent Component { get; private set; }
    
    public UIComponentAddPacket(Identifier viewIdentifier, UINetworkComponent component)
    {
        ViewIdentifier = viewIdentifier;
        Component = component;
    }
}