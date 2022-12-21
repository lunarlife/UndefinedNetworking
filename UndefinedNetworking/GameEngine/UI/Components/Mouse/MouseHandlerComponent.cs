using UndefinedNetworking.Events.Mouse;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.UI.Components.Mouse;

public abstract record MouseHandlerComponent<T> : UINetworkComponent, IEventCaller<T> where T : UIMouseEvent
{
    internal MouseHandlerComponent()
    {
        
    }
}