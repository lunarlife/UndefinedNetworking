using UndefinedServer.UI.Windows;
using Utils.Events;

namespace UndefinedServer.Events.UI.WindowEvents
{
    public abstract class WindowEvent : Event
    {
        public abstract Window Window { get; }
    }
}