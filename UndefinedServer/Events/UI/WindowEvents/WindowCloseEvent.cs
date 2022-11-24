using UndefinedServer.UI.Windows;

namespace UndefinedServer.Events.UI.WindowEvents;

public class WindowCloseEvent : WindowEvent
{
    public override Window Window { get; }
    public WindowCloseEvent(Window window)
    {
        Window = window;
    }

}