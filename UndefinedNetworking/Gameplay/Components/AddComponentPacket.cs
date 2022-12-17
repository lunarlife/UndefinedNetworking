using Networking;
using Networking.Packets;
using UndefinedNetworking.GameEngine.UI.Components;

namespace UndefinedNetworking.Gameplay.Components;

public class AddComponentPacket : Packet
{
    public INetworkComponent Component { get; private set; }
    public Identifier Identifier { get; private set; }
    
    public AddComponentPacket(INetworkComponent component)
    {
        Component = component;
    }
}