using UndefinedNetworking.Window.UI;
using Utils.Events;

namespace UndefinedNetworking.Events.Window.UI;

public abstract class UIEvent<T> : Event where T : IUIElement
{
    public abstract T Element { get; }
}