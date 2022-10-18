using Networking.Events.Window;
using Utils.Events;

namespace Networking.Window;

public interface IClosable<T> : IEventCaller<WindowCloseEvent<T>> where T : IWindow
{
    public bool CanCloseNow { get; }
}