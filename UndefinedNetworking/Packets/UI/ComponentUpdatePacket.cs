using Networking.Packets;
using UndefinedNetworking.GameEngine.UI.Components;

namespace UndefinedNetworking.Packets.UI;

public class ComponentUpdatePacket : Packet
{
    public INetworkComponent Component { get; private set; }
    
    public ComponentUpdatePacket(INetworkComponent component)
    {
        Component = component;
    }
}