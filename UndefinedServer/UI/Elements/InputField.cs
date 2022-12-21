using UndefinedServer.Events.UI.InputFieldEvents;
using Utils.Events;

namespace UndefinedServer.UI.Elements;

public class InputField :
    IEventCaller<InputFieldDeselectEvent>, IEventCaller<InputFieldSelectEvent>, IEventCaller<InputFieldWritingEvent>, IEventCaller<InputFieldContentChangedEvent>
{
}