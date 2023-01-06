using UndefinedNetworking.GameEngine.Scenes.UI;

namespace UndefinedNetworking.Events.Mouse;

public class UIMouseExitEvent : UIMouseEvent
{
    public override IUIView View { get; }
    public UIMouseExitEvent(IUIView view)
    {
        View = view;
    }

}