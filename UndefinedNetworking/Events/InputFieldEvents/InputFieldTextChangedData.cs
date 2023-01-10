using UndefinedNetworking.GameEngine.Scenes.UI.Components;

namespace UndefinedNetworking.Events.InputFieldEvents;

public class InputFieldTextChangedData : InputFieldEventData
{
    public override InputField InputField { get; }
    
    public InputFieldTextChangedData(InputField inputField)
    {
        InputField = inputField;
    }
}