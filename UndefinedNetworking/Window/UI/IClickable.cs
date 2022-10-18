using Networking.Events.Window.UI;
using Utils.Events;

namespace Networking.Window.UI;

public interface IClickable<T> : IEventCaller<UIClickEvent<T>> where T : IUIElement
{
    public bool IsCanClickNow { get; }
}