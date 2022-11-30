using Utils.Events;

namespace UndefinedServer.Events.PlayerEvents
{
    public abstract class PlayerEvent : Event
    {
        public abstract UndefinedServer.Player Player { get; }
    }
}