using UndefinedServer.UI.Elements;

namespace UndefinedServer.Events.UI.InputFieldEvents;

public class InputFieldContentChangedEvent : InputFieldEvent
{
    public override InputField InputField { get; }
    
    public InputFieldContentChangedEvent(InputField inputField)
    {
        InputField = inputField;
    }

}