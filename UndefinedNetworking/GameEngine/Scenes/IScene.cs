using UndefinedNetworking.Events.SceneEvents;
using UndefinedNetworking.Events.UIEvents;
using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.Scenes;

public interface IScene
{
    public Event<SceneUnloadEventData> SceneUnload { get; }
    public Event<UIOpenEventData> UIOpen { get; }
    public ISceneViewer Viewer { get; }
    public IObjectBase[] Objects { get; }
    public SceneType Type { get; }
    public void Unload();
    public IUIView OpenView(ViewParameters parameters);
    public IUIViewBase GetView(uint identifier);
    public bool TryGetView(uint identifier, out IUIViewBase view);
    public void OpenView(IMultipleUIView view);
    public bool ContainsView(IUIViewBase view);
}