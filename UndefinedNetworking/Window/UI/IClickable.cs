using UndefinedNetworking.Events.Window.UI;
using Utils.Events;

namespace UndefinedNetworking.Window.UI;

public interface IClickable<T> : IEventCaller<UIClickEvent<T>> where T : IUIElement
{
    public bool IsCanClickNow { get; }
}