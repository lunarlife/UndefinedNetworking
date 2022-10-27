using UndefinedNetworking.GameEngine.UI.Windows.Interfaces;

namespace UndefinedNetworking.Events.UI.Window
{
    public class WindowFloatingEvent : WindowEvent
    {
        public override IWindow Window { get; }
    
        public WindowFloatingEvent(IWindow window)
        {
            Window = window;
        }
    }
}