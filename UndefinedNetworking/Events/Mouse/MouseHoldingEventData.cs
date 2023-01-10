using UndefinedNetworking.GameEngine.Scenes.UI;

namespace UndefinedNetworking.Events.Mouse;

public class MouseHoldingEventData : MouseEventData
{
    public override IUIView View { get; }
    public MouseHoldingEventData(IUIView view)
    {
        View = view;
    }
}