using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;

namespace UndefinedNetworking.Events.Mouse;

public class MouseUpEventData : MouseEventData
{
    public override IUIViewBase View { get; }
    public MouseUpEventData(IUIViewBase view)
    {
        View = view;
    }

}