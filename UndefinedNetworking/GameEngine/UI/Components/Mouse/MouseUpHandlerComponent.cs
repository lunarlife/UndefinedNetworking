using Networking.DataConvert;
using Networking.DataConvert.Handlers;
using UndefinedNetworking.Events.Mouse;
using UndefinedNetworking.GameEngine.Input;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.UI.Components.Mouse;
    
public sealed record MouseUpHandlerComponent : MouseHandlerComponent<UIMouseUpEvent>, IDeserializeHandler
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

    public void OnDeserialize()
    {
        this.CallEvent(new UIMouseUpEvent(TargetView));
    }
}