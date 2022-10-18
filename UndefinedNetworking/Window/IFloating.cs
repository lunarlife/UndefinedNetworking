using UndefinedNetworking.Events.Window;
using Utils.Events;

namespace UndefinedNetworking.Window;

public interface IFloating<T> : IEventCaller<WindowFloatingEvent<T>> where T : IWindow
{
    public bool IsCanFloatingNow { get; }
}