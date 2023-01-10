using UndefinedNetworking.GameEngine.Scenes.UI.Components;
using Utils.Events;

namespace UndefinedNetworking.Events.InputFieldEvents;

public abstract class InputFieldEventData : EventData
{
    public abstract InputField InputField { get; }
}