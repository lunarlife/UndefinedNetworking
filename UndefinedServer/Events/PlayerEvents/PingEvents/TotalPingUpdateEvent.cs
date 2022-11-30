namespace UndefinedServer.Events.PlayerEvents.PingEvents;

public sealed class TotalPingUpdateEvent : PingUpdateEvent
{
    public TotalPingUpdateEvent(Player player) : base(player)
    {
    }
}