using UndefinedNetworking.GameEngine.Scenes;
using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;

namespace UndefinedNetworking.Events.UIEvents;

public class UICloseEventData : UIEventData
{
    public override IUIViewBase View { get; }
    public override ISceneViewer Viewer { get; }

    public UICloseEventData(IUIViewBase view, ISceneViewer viewer)
    {
        View = view;
        Viewer = viewer;
    }

}