using UndefinedNetworking.GameEngine.Scenes.UI;

namespace UndefinedNetworking.Events.UIEvents;

public class UIOpenEventData : UIEventData
{
    public override IUIView View { get; }

    public UIOpenEventData(IUIView view)
    {
        View = view;
    }

}