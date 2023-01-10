using UndefinedNetworking.Events.UIEvents;
using UndefinedNetworking.GameEngine.Scenes;
using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;

namespace UndefinedServer.UI.View;

public class PlayerUIView : UIView, IUIView
{
    public ISceneViewer Viewer { get; }
    
    public PlayerUIView(ISceneViewer viewer, ViewParameters parameters) : base(parameters)
    {
        Viewer = viewer;
    }

    public override void Destroy()
    {
        OnClose.Invoke(new UICloseEventData(this, Viewer));
    }

    public override bool ContainsViewer(ISceneViewer viewer) => Viewer == viewer;
}