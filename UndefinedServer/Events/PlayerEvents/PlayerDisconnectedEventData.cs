using UndefinedNetworking;

namespace UndefinedServer.Events.PlayerEvents;

public class PlayerDisconnectedEventData : PlayerEventData
{
    public override Player Player { get; }
    public DisconnectCause Cause { get; }
    public string Message { get; }

    public PlayerDisconnectedEventData(Player player, DisconnectCause cause, string message)
    {
        Player = player;
        Cause = cause;
        Message = message;
    }
}