using UndefinedNetworking.Events.ObjectEvents;
using UndefinedNetworking.Events.SceneEvents;
using UndefinedNetworking.Events.UIEvents;
using UndefinedNetworking.GameEngine.Scenes.Objects;
using UndefinedNetworking.GameEngine.Scenes.UI;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.Scenes;

public interface IScene : IEventCaller<SceneUnloadEvent>, IEventCaller<SceneLoadEvent>, IEventCaller<UIOpenEvent>, IEventCaller<UICloseEvent>, IEventCaller<ObjectInstantiateEvent>, IEventCaller<ObjectDestroyEvent>    
{
    public ISceneViewer Viewer { get; }
    public IObjectBase[] Objects { get; }
    public SceneType Type { get; }
    public void Unload();
    public IUIView OpenView(ViewParameters parameters);
    public void CloseView(IUIView view);
    public void DestroyObject(IGameObject obj);
    public IUIView GetView(uint identifier);
    public bool TryGetView(uint identifier, out IUIView view);
}