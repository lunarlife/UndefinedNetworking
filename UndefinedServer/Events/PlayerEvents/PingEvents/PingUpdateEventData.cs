namespace UndefinedServer.Events.PlayerEvents.PingEvents;

public abstract class PingUpdateEventData : PlayerEventData
{
    public override Player Player { get; }

    public PingUpdateEventData(Player player)
    {
        Player = player;
    }
}