using UndefinedNetworking.Events.Window;
using Utils.Events;

namespace UndefinedNetworking.Window;

public interface IClosable<T> : IEventCaller<WindowCloseEvent<T>> where T : IWindow
{
    public bool CanCloseNow { get; }
}