using Networking.DataConvert.Handlers;
using UndefinedNetworking.Events.Mouse;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components.Mouse;

public sealed record MouseExitHandler : MouseHandler<MouseExitEventData>, IDeserializeHandler
{
    public void OnDeserialize()
    {
        Event.Invoke(new MouseExitEventData(TargetObject));
    }
}