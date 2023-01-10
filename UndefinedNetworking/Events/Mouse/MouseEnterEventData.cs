using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;

namespace UndefinedNetworking.Events.Mouse;

public class MouseEnterEventData : MouseEventData
{
    public override IUIViewBase View { get; }
    public MouseEnterEventData(IUIViewBase view)
    {
        View = view;
    }

}