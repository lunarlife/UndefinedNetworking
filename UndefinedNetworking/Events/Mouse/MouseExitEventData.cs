using UndefinedNetworking.GameEngine.Scenes.UI;

namespace UndefinedNetworking.Events.Mouse;

public class MouseExitEventData : MouseEventData
{
    public override IUIView View { get; }
    public MouseExitEventData(IUIView view)
    {
        View = view;
    }

}