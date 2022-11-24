using UndefinedServer.UI.Elements;
using Utils.Events;

namespace UndefinedServer.Events.UI.InputFieldEvents
{
    public abstract class InputFieldEvent : Event
    {
        public abstract InputField InputField { get; }
    }
}