using UndefinedNetworking.Events.Mouse;
using UndefinedNetworking.GameEngine.Input;

namespace UndefinedNetworking.GameEngine.UI.Components.Mouse;

public sealed record MouseHoldingHandlerComponent : MouseHandlerComponent<UIMouseHoldingEvent>
{
    [ClientData] public MouseKey Keys { get; set; }
    [ServerData] public bool IsHolding { get; set; }

}