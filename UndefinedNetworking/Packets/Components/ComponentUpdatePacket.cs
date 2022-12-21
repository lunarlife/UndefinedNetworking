using Networking;
using UndefinedNetworking.GameEngine.UI.Components;

namespace UndefinedNetworking.Packets.Components;

public class ComponentUpdatePacket
{
    public Identifier Identifier { get; }
    public Identifier ViewIdentifier { get; }
    public INetworkComponent Component { get; }
    
    public ComponentUpdatePacket(Identifier identifier, Identifier viewIdentifier, INetworkComponent component)
    {
        Identifier = identifier;
        Component = component;
        ViewIdentifier = viewIdentifier;
    }
}