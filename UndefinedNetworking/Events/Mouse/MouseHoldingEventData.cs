using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;

namespace UndefinedNetworking.Events.Mouse;

public class MouseHoldingEventData : MouseEventData
{
    public override IUIViewBase View { get; }
    public MouseHoldingEventData(IUIViewBase view)
    {
        View = view;
    }
}