using UndefinedNetworking.GameEngine.Scenes.UI;

namespace UndefinedNetworking.Events.UIEvents;

public class UICloseEventData : UIEventData
{
    public override IUIView View { get; }

    public UICloseEventData(IUIView view)
    {
        View = view;
    }

}