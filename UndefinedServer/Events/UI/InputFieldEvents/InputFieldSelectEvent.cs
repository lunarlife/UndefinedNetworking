using UndefinedServer.UI.Elements;

namespace UndefinedServer.Events.UI.InputFieldEvents
{
    public class InputFieldSelectEvent : InputFieldEvent
    {
        public override InputField InputField { get; }

        public InputFieldSelectEvent(InputField inputField)
        {
            InputField = inputField;
        }

    }
}