using Networking.DataConvert.Handlers;
using UndefinedNetworking.Events.Mouse;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.UI.Components.Mouse;

public sealed record MouseEnterHandlerComponent : MouseHandlerComponent<UIMouseEnterEvent>, IDeserializeHandler
{
    public void OnDeserialize()
    {
        this.CallEvent(new UIMouseEnterEvent(TargetView));
    }
}