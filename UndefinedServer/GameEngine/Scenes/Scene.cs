using System.Collections.Generic;
using System.Linq;
using Networking;
using UndefinedNetworking.Events.ObjectEvents;
using UndefinedNetworking.Events.SceneEvents;
using UndefinedNetworking.Events.UIEvents;
using UndefinedNetworking.Exceptions;
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
    private readonly Dictionary<Identifier, IObjectBase> _objects = new();
    public ISceneViewer Viewer { get; }

    public IEnumerable<IObjectBase> Objects => _objects.Values;

    public abstract SceneType Type { get; }
    
    protected Scene(ISceneViewer viewer)
    {
        Viewer = viewer;
        this.CallEvent(new SceneLoadEvent(this));
    }

    public void CloseView(IUIView view)
    {
        if (!Objects.Contains(view)) throw new ObjectException("unknown object");
        _objects.Remove(view.Identifier);
        this.CallEvent(new UICloseEvent(view));
    }

    public IUIView OpenView(ViewParameters parameters)
    {
        var view = new UIView(Viewer, parameters);
        _objects.Add(view.Identifier, view);
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
        if (!Objects.Contains(obj)) throw new ObjectException("unknown object");
        _objects.Remove(obj.Identifier);
        this.CallEvent(new ObjectDestroyEvent(obj));
    }

    public IUIView GetView(Identifier identifier) =>
        !_objects.ContainsKey(identifier) || _objects[identifier] is not IUIView view
            ? throw new ViewException("view not found")
            : view;

    public bool TryGetView(Identifier identifier, out IUIView? view)
    {
        if (!_objects.ContainsKey(identifier) || _objects[identifier] is not IUIView v)
        {
            view = null;
            return false;
        }
        view = v;
        return true;
    }
}