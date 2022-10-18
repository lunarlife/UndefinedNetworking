using Networking.Events.Window;
using Utils.Events;

namespace Networking.Window;

public interface IFloating<T> : IEventCaller<WindowFloatingEvent<T>> where T : IWindow
{
    public bool IsCanFloatingNow { get; }
}