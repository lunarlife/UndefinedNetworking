using UndefinedNetworking.Window;
using Utils.Events;

namespace UndefinedNetworking.Events.Window;

public abstract class WindowEvent<T> : Event where T : IWindow
{
    public abstract T Window { get; }
}