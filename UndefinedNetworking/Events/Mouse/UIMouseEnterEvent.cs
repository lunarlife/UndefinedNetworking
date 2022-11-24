using UndefinedNetworking.GameEngine.UI;

namespace UndefinedNetworking.Events.Mouse;

public class UIMouseEnterEvent : UIMouseEvent
{
    public override IUIView View { get; }
    public UIMouseEnterEvent(IUIView view)
    {
        View = view;
    }

}