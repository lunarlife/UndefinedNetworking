using UndefinedServer.UI.Windows;

namespace UndefinedServer.Events.UI.WindowEvents;

public class WindowFloatingEvent : WindowEvent
{
    public override Window Window { get; }
    public WindowFloatingEvent(Window window)
    {
        Window = window;
    }

}