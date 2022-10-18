using Utils.Events;

namespace UndefinedServer.Events.Player
{
    public abstract class PlayerEvent : Event
    {
        public abstract ServerPlayer Player { get; }
    }
}