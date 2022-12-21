using System.Collections.Generic;
using UndefinedNetworking.Events.ObjectEvents;
using UndefinedNetworking.Events.SceneEvents;
using UndefinedNetworking.Events.UIEvents;
using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.Objects;
using UndefinedNetworking.GameEngine.Scenes;
using UndefinedNetworking.GameEngine.UI;
using UndefinedServer.Exeptions;
using UndefinedServer.UI.View;
using Utils.Events;

namespace UndefinedServer.GameEngine.Scenes;

public abstract class Scene<T> : IScene where T : IGameObject
{
    private readonly List<IObjectBase> _objects = new();
    public ISceneViewer Viewer { get; }

    public IEnumerable<IObjectBase> Objects => _objects;

    public abstract SceneType Type { get; }
    
    protected Scene(ISceneViewer viewer)
    {
        Viewer = viewer;
        this.CallEvent(new SceneLoadEvent(this));
    }

    public void CloseView(IUIView view)
    {
        if (!_objects.Contains(view)) throw new ObjectException("unknown object");
        _objects.Remove(view);
        EventManager.CallEvent(new UICloseEvent(view));
    }

    public IUIView OpenView(ViewParameters parameters)
    {
        var view = new UIView(Viewer, parameters);
        this.CallEvent(new UIOpenEvent(view));
        return view;
    }

    public void Instantiate<T1>() where T1 : T, new()
    {
        
    }
    
    public void Unload()
    {
        this.CallEvent(new SceneUnloadEvent(this));
    }
    
    public void DestroyObject(IGameObject obj)
    {
        if (!_objects.Contains(obj)) throw new ObjectException("unknown object");
        _objects.Remove(obj);
        this.CallEvent(new ObjectDestroyEvent(obj));
    }
}