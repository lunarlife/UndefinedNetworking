using Networking;
using Networking.Packets;
using UndefinedNetworking.GameEngine.Scenes.UI.Components;

namespace UndefinedNetworking.Packets.Components;

public class UIComponentAddPacket : Packet
{
    public Identifier ViewIdentifier { get; }
    public UINetworkComponentData Component { get; private set; }
    
    public UIComponentAddPacket(Identifier viewIdentifier, UINetworkComponentData component)
    {
        ViewIdentifier = viewIdentifier;
        Component = component;
    }
}