using System.Collections.Generic;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.GameEngine.UI.Components;
using UndefinedServer.Events.UI.InputFieldEvents;
using Utils.Events;

namespace UndefinedServer.UI.Elements;

public class InputField : UIElement,
    IEventCaller<InputFieldDeselectEvent>, IEventCaller<InputFieldSelectEvent>, IEventCaller<InputFieldWritingEvent>, IEventCaller<InputFieldContentChangedEvent>
{
    private readonly UIComponent[] _components =
    {
        new TextComponent()
    };
    
    public InputField(UIElement? parent) : base(parent)
    {
    }

    public override IEnumerable<UIComponent> Components => _components;
    public override ViewParameters CreateNewView(IUIViewer viewer) => new();
}