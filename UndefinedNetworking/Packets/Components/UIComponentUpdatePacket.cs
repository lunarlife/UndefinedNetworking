using Networking;
using Networking.Packets;
using UndefinedNetworking.GameEngine.UI.Components;

namespace UndefinedNetworking.Packets.Components;

public class UIComponentUpdatePacket : Packet
{
    public UINetworkComponent Component { get; private set; }
    
    public UIComponentUpdatePacket(UINetworkComponent component)
    {
        Component = component;
    }
}