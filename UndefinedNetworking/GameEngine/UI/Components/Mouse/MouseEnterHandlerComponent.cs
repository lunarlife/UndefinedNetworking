using Networking.DataConvert;
using UndefinedNetworking.Events.Mouse;

namespace UndefinedNetworking.GameEngine.UI.Components.Mouse;

public sealed record MouseEnterHandlerComponent : MouseHandlerComponent<UIMouseEnterEvent>
{
    [ExcludeData] public bool IsEntered { get; set; }

}