using UndefinedNetworking.GameEngine.Scenes;
using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;
using Utils.Events;

namespace UndefinedNetworking.Events.UIEvents;

public abstract class UIEventData : EventData
{
    public abstract IUIViewBase View { get; }
    public abstract ISceneViewer Viewer { get; }

}