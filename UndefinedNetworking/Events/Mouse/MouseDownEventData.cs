using UndefinedNetworking.GameEngine.Scenes.UI;

namespace UndefinedNetworking.Events.Mouse;

public class MouseDownEventData : MouseEventData
{
    public override IUIView View { get; }
    public MouseDownEventData(IUIView view)
    {
        View = view;
    }

}