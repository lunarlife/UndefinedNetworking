using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;

namespace UndefinedNetworking.Events.Mouse;

public class MouseDownEventData : MouseEventData
{
    public override IUIViewBase View { get; }
    public MouseDownEventData(IUIViewBase view)
    {
        View = view;
    }

}