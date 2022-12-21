using UndefinedNetworking.GameEngine.UI;

namespace UndefinedNetworking.Events.UIEvents;

public class UIOpenEvent : UIEvent
{
    public override IUIView View { get; }

    public UIOpenEvent(IUIView view)
    {
        View = view;
    }

}