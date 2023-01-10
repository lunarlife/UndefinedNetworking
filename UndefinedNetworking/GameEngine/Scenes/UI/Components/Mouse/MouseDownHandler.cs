using Networking.DataConvert.Handlers;
using UndefinedNetworking.Events.Mouse;
using UndefinedNetworking.GameEngine.Input;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components.Mouse;

public sealed record MouseDownHandler : MouseHandler<MouseDownEventData>, IDeserializeHandler
{
    [ClientData]
    public MouseKey Keys { get; set; }
    
    public void OnDeserialize()
    {
        Event.Invoke(new MouseDownEventData(TargetView));
    }
}

