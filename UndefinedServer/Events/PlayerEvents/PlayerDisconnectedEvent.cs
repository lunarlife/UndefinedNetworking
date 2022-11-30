using UndefinedNetworking;

namespace UndefinedServer.Events.PlayerEvents;

public class PlayerDisconnectedEvent : PlayerEvent
{
    public override Player Player { get; }
    public DisconnectCause Cause { get; }
    public string Message { get; }

    public PlayerDisconnectedEvent(Player player, DisconnectCause cause, string message)
    {
        Player = player;
        Cause = cause;
        Message = message;
    }
}