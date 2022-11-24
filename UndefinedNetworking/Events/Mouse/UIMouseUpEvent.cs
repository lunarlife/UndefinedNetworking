using UndefinedNetworking.GameEngine.UI;

namespace UndefinedNetworking.Events.Mouse;

public class UIMouseUpEvent : UIMouseEvent
{
    public override IUIView View { get; }
    public UIMouseUpEvent(IUIView view)
    {
        View = view;
    }

}