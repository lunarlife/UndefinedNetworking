using Networking.Window.UI;
using Utils.Events;

namespace Networking.Events.Window.UI;

public abstract class UIEvent<T> : Event where T : IUIElement
{
    public abstract T Element { get; }
}