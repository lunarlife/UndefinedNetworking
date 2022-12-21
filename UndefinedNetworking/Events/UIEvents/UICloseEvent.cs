using UndefinedNetworking.GameEngine.UI;

namespace UndefinedNetworking.Events.UIEvents;

public class UICloseEvent : UIEvent
{
    public override IUIView View { get; }

    public UICloseEvent(IUIView view)
    {
        View = view;
    }

}