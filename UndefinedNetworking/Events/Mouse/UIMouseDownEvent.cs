using UndefinedNetworking.GameEngine.Scenes.UI;

namespace UndefinedNetworking.Events.Mouse;

public class UIMouseDownEvent : UIMouseEvent
{
    public override IUIView View { get; }
    public UIMouseDownEvent(IUIView view)
    {
        View = view;
    }

}