using Networking.DataConvert.Handlers;
using UndefinedNetworking.Events.Mouse;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components.Mouse;

public sealed record MouseExitHandlerComponent : MouseHandlerComponent<UIMouseExitEvent>, IDeserializeHandler
{
    public void OnDeserialize()
    {
        this.CallEvent(new UIMouseExitEvent(TargetView));
    }
}