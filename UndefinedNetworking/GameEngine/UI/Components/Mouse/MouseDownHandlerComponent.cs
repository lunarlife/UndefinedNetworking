using UndefinedNetworking.Events.Mouse;
using UndefinedNetworking.GameEngine.Input;

namespace UndefinedNetworking.GameEngine.UI.Components.Mouse;

public record MouseDownHandlerComponent : MouseHandlerComponent<UIMouseDownEvent>
{
    public MouseKey Keys { get; init; }
}

