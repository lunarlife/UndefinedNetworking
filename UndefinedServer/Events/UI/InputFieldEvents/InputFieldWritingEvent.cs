using UndefinedServer.UI.Elements;

namespace UndefinedServer.Events.UI.InputFieldEvents
{
    public class InputFieldWritingEvent : InputFieldEvent
    {
        public override InputField InputField { get; }

        public InputFieldWritingEvent(InputField inputField)
        {
            InputField = inputField;
        }

    }
}