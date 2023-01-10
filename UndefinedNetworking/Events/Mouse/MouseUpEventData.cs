using UndefinedNetworking.GameEngine.Scenes.UI;

namespace UndefinedNetworking.Events.Mouse;

public class MouseUpEventData : MouseEventData
{
    public override IUIView View { get; }
    public MouseUpEventData(IUIView view)
    {
        View = view;
    }

}