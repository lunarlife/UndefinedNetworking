using System.Collections.Generic;
using Networking;
using UndefinedNetworking.Events.ObjectEvents;
using UndefinedNetworking.Events.SceneEvents;
using UndefinedNetworking.Events.UIEvents;
using UndefinedNetworking.GameEngine.Objects;
using UndefinedNetworking.GameEngine.UI;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.Scenes;

public interface IScene : IEventCaller<SceneUnloadEvent>, IEventCaller<SceneLoadEvent>, IEventCaller<UIOpenEvent>, IEventCaller<UICloseEvent>, IEventCaller<ObjectInstantiateEvent>, IEventCaller<ObjectDestroyEvent>    
{
    public ISceneViewer Viewer { get; }
    public IEnumerable<IObjectBase> Objects { get; }
    public SceneType Type { get; }
    public void Unload();
    public IUIView OpenView(ViewParameters parameters);
    public void CloseView(IUIView view);
    public void DestroyObject(IGameObject obj);
    public IUIView GetView(Identifier identifier);
    public bool TryGetView(Identifier identifier, out IUIView view);
}