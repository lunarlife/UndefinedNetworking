using UndefinedNetworking.GameEngine.Scenes.UI.Components;
using Utils.Events;

namespace UndefinedNetworking.Events.InputFieldEvents;

public abstract class InputFieldEvent : Event
{
    public abstract InputFieldComponent InputField { get; }
}