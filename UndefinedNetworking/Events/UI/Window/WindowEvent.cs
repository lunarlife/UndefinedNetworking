using UndefinedNetworking.GameEngine.UI.Windows.Interfaces;
using Utils.Events;

namespace UndefinedNetworking.Events.UI.Window
{
    public abstract class WindowEvent : Event
    {
        public abstract IWindow Window { get; }
    }
}