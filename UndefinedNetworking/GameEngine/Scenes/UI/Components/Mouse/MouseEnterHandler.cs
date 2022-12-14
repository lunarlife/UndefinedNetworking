using Networking.DataConvert.Handlers;
using UndefinedNetworking.Events.Mouse;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components.Mouse;

public sealed record MouseEnterHandler : MouseHandler<MouseEnterEventData>, IDeserializeHandler
{
    public void OnDeserialize()
    {
        Event.Invoke(new MouseEnterEventData(TargetObject));
    }
}