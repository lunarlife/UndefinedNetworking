using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;

namespace UndefinedNetworking.Events.Mouse;

public class MouseExitEventData : MouseEventData
{
    public override IUIViewBase View { get; }
    public MouseExitEventData(IUIViewBase view)
    {
        View = view;
    }

}