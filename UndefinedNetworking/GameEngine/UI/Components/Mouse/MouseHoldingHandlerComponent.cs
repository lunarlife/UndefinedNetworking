using Networking.DataConvert;
using UndefinedNetworking.Events.Mouse;
using UndefinedNetworking.GameEngine.Input;

namespace UndefinedNetworking.GameEngine.UI.Components.Mouse;

public sealed record MouseHoldingHandlerComponent : MouseHandlerComponent<UIMouseHoldingEvent>
{
    [ClientData] private MouseKey _keys;

    [ExcludeData]
    public MouseKey Keys
    {
        get => _keys;
        set
        {
            _keys = value;
            Update();
        }
    }

    [ServerData] public bool IsHolding { get; set; }
}