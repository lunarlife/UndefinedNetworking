using Networking.Packets;
using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Scenes.UI.Components;

namespace UndefinedNetworking.Packets.Components;

public class UIComponentUpdatePacket : Packet
{
    public IComponent<UINetworkComponentData> Component { get; private set; }
    
    public UIComponentUpdatePacket(IComponent<UINetworkComponentData> component)
    {
        Component = component;
    }
}