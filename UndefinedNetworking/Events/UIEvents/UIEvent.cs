using UndefinedNetworking.GameEngine.UI;
using Utils.Events;

namespace UndefinedNetworking.Events.UIEvents;

public abstract class UIEvent : Event
{
    public abstract IUIView View { get; }
}