using System.Collections.Generic;
using System.Linq;
using UndefinedNetworking.Events.ObjectEvents;
using UndefinedNetworking.Events.SceneEvents;
using UndefinedNetworking.Events.UIEvents;
using UndefinedNetworking.Exceptions;
using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.Scenes;
using UndefinedNetworking.GameEngine.Scenes.Objects;
using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;
using UndefinedServer.Exceptions;
using UndefinedServer.UI.View;
using Utils.Events;

namespace UndefinedServer.GameEngine.Scenes;

public abstract class Scene<T> : IScene where T : IGameObject
{
    private readonly List<IObjectBase> _objects = new();
    public Event<SceneUnloadEventData> SceneUnload { get; } = new();
    public Event<UIOpenEventData> UIOpen { get; } = new();
    public Event<ObjectDestroyEventData> ObjectDestroy { get; } = new();
    public ISceneViewer Viewer { get; }

    public IObjectBase[] Objects => _objects.ToArray();

    public abstract SceneType Type { get; }
    
    protected Scene(ISceneViewer viewer)
    {
        Viewer = viewer;
    }

    public void CloseView(IUIViewBase view)
    {
        if (!Objects.Contains(view)) throw new ObjectException("unknown object");
        _objects.Remove(view);
    }

    public IUIView OpenView(ViewParameters parameters)
    {
        var view = new PlayerUIView(Viewer, parameters);
        _objects.Add(view);
        UIOpen.Invoke(new UIOpenEventData(view, Viewer));
        return view;
    }

    public void Instantiate<T1>() where T1 : T, new()
    {
        
    }
    
    public void Unload()
    {
        _objects.Clear();
        SceneUnload.Invoke(new SceneUnloadEventData(this));
    }
    
    public void DestroyObject(IGameObject obj)
    {
        if (!Objects.Contains(obj)) throw new ObjectException("unknown object");
        _objects.Remove(obj);
        ObjectDestroy.Invoke(new ObjectDestroyEventData(obj));
    }

    public IUIViewBase GetView(uint identifier) =>
        _objects.Count > identifier || _objects[(int)identifier] is not IUIViewBase view
            ? throw new ViewException("view not found")
            : view;

    public bool TryGetView(uint identifier, out IUIViewBase view)
    {
        if (_objects.Count > identifier || _objects[(int)identifier] is not IUIViewBase v)
        {
            view = null;
            return false;
        }
        view = v;
        return true;
    }

    public void OpenView(IMultipleUIView view)
    {
        _objects.Add(view);
        UIOpen.Invoke(new UIOpenEventData(view, Viewer));
    }

    public bool ContainsView(IUIViewBase view) => _objects.Contains(view);
}