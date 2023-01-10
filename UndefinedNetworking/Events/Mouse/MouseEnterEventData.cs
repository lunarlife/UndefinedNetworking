using UndefinedNetworking.GameEngine.Scenes.UI;

namespace UndefinedNetworking.Events.Mouse;

public class MouseEnterEventData : MouseEventData
{
    public override IUIView View { get; }
    public MouseEnterEventData(IUIView view)
    {
        View = view;
    }

}