using Networking.Window;
using Utils.Events;

namespace Networking.Events.Window;

public abstract class WindowEvent<T> : Event where T : IWindow
{
    public abstract T Window { get; }
}