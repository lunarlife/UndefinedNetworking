using UndefinedNetworking.GameEngine.UI.Windows.Interfaces;

namespace UndefinedNetworking.Events.UI.Window;

public class WindowCloseEvent : WindowEvent
{
    public override IWindow Window { get; }
    public WindowCloseEvent(IWindow window)
    {
        Window = window;
    }

}