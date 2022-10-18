using Networking.Window;

namespace Networking.Events.Window;

public class WindowCloseEvent<T> : WindowEvent<T> where T : IWindow
{
    public override T Window { get; }
    public WindowCloseEvent(T window)
    {
        Window = window;
    }

}