using System.Collections.Generic;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.GameEngine.UI.Components;
using UndefinedServer.Events.UI.InputFieldEvents;
using Utils.Events;

namespace UndefinedServer.UI.Elements;

public class InputField : UIElement,
    IEventCaller<InputFieldDeselectEvent>, IEventCaller<InputFieldSelectEvent>, IEventCaller<InputFieldWritingEvent>, IEventCaller<InputFieldContentChangedEvent>
{
    public InputField(UIElement? parent) : base(parent)
    {
    }

    public override ViewParameters CreateNewView(IUIViewer viewer) => new();
}