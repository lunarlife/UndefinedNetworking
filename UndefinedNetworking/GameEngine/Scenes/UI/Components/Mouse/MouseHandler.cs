using UndefinedNetworking.Events.Mouse;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components.Mouse;

public abstract record MouseHandler<T> : UINetworkComponentData where T : MouseEventData
{
    public Event<T> Event { get; } = new();

    internal MouseHandler()
    {
        
    }
}