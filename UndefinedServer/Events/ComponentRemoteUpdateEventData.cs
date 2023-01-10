using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Scenes;
using Utils.Events;

namespace UndefinedServer.Events;

public class ComponentRemoteUpdateEventData : EventData
{
    public IComponent Component { get; }

    internal ComponentRemoteUpdateEventData(IComponent component)
    {
        Component = component;
    }
}