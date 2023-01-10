using Networking.DataConvert.Handlers;
using UndefinedNetworking.Events.Mouse;
using UndefinedNetworking.GameEngine.Input;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components.Mouse;
    
public sealed record MouseUpHandler : MouseHandler<MouseUpEventData>, IDeserializeHandler
{
    [ClientData]
    public MouseKey Keys { get; set; }

    public void OnDeserialize()
    {
        Event.Invoke(new MouseUpEventData(TargetObject));

    }
}