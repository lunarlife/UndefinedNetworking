using UndefinedNetworking.GameEngine.Scenes.UI.Components;

namespace UndefinedNetworking.Events.InputFieldEvents;

public class InputFieldTextChangedEvent : InputFieldEvent
{
    public override InputFieldComponent InputField { get; }
    
    public InputFieldTextChangedEvent(InputFieldComponent inputField)
    {
        InputField = inputField;
    }
}