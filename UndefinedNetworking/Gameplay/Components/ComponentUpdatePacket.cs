using Networking;
using Networking.Packets;
using UndefinedNetworking.GameEngine.UI.Components;

namespace UndefinedNetworking.Gameplay.Components;

public class ComponentUpdatePacket : Packet
{
    public INetworkComponent Component { get; private set; }
    public Identifier Identifier { get; private set; }
    public ComponentUpdatePacket(INetworkComponent component, Identifier identifier)
    {
        Component = component;
        Identifier = identifier;
    }
}