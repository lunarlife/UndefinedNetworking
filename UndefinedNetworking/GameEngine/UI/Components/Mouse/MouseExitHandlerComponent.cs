using Networking.DataConvert;
using UndefinedNetworking.Events.Mouse;

namespace UndefinedNetworking.GameEngine.UI.Components.Mouse;

public sealed record MouseExitHandlerComponent : MouseHandlerComponent<UIMouseExitEvent>
{
    [ExcludeData] public bool IsExited { get; set; }
}