using Utils.Events;

namespace UndefinedServer.Events.PlayerEvents
{
    public abstract class PlayerEventData : EventData
    {
        public abstract Player Player { get; }
    }
}