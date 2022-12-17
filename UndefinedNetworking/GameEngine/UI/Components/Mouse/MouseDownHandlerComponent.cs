using UndefinedNetworking.Events.Mouse;
using UndefinedNetworking.GameEngine.Input;

namespace UndefinedNetworking.GameEngine.UI.Components.Mouse;

public sealed record MouseDownHandlerComponent : MouseHandlerComponent<UIMouseDownEvent>
{
    [ClientData] public MouseKey Keys { get; set; }

}

