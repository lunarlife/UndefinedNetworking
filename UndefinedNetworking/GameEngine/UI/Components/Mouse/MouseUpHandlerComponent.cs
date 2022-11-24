using UndefinedNetworking.Events.Mouse;
using UndefinedNetworking.GameEngine.Input;

namespace UndefinedNetworking.GameEngine.UI.Components.Mouse;
    
public record MouseUpHandlerComponent : MouseHandlerComponent<UIMouseEnterEvent>
{
    public MouseKey Keys { get; }
}