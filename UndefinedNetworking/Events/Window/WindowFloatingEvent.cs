using Networking.Window;

namespace Networking.Events.Window;

public class WindowFloatingEvent<T> : WindowEvent<T> where T : IWindow
{
    public override T Window { get; }
    
    public WindowFloatingEvent(T window)
    {
        Window = window;
    }
}