namespace UndefinedServer.Events.PlayerEvents;

public class PlayerResourcesDownloadedEventData : PlayerEventData
{
    public override Player Player { get; }

    public PlayerResourcesDownloadedEventData(Player player)
    {
        Player = player;
    }
}