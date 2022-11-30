namespace UndefinedServer.Events.PlayerEvents.PingEvents;

public abstract class PingUpdateEvent : PlayerEvent
{
    public override Player Player { get; }

    public PingUpdateEvent(Player player)
    {
        Player = player;
    }
}