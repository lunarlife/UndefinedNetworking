using UndefinedNetworking.Events.Mouse;
using UndefinedNetworking.GameEngine.Input;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components.Mouse;

public sealed record MouseHoldingHandler : MouseHandler<MouseHoldingEventData>
{
    [ClientData]
    public MouseKey Keys { get; set; }

    [ServerData] public bool IsHolding { get; set; }
}