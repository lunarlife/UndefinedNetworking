using UndefinedNetworking.GameEngine.UI;

namespace UndefinedNetworking.Events.Mouse;

public class UIMouseHoldingEvent : UIMouseEvent
{
    public override IUIView View { get; }
    public UIMouseHoldingEvent(IUIView view)
    {
        View = view;
    }

}