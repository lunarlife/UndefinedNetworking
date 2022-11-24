
using UndefinedServer.UI.Elements;

namespace UndefinedServer.Events.UI.InputFieldEvents
{
    public class InputFieldDeselectEvent : InputFieldEvent
    {
        public override InputField InputField { get; }

        public InputFieldDeselectEvent(InputField inputField)
        {
            InputField = inputField;
        }

    }
}