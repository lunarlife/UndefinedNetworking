using System.Collections.Generic;
using UndefinedNetworking.Events.UIEvents;
using UndefinedNetworking.Exceptions;
using UndefinedNetworking.GameEngine.Scenes;
using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;
using UndefinedServer.Events;
using UndefinedServer.Events.PlayerEvents;
using Utils.Events;

namespace UndefinedServer.UI.View;

public class MultipleUIView : UIView, IMultipleUIView, IEventListener
{
    private readonly List<ISceneViewer> _viewers = new();
    
    private Event<ComponentRemoteUpdateEventData> OnRemoteUpdate { get; } = new();
    public IEnumerable<ISceneViewer> Viewers => _viewers;
    
    public MultipleUIView(ViewParameters parameters) : base(parameters)
    {
        EventManager.RegisterEvents(this);
    }
 
    
    public void Close(ISceneViewer viewer)
    {
        if (!_viewers.Contains(viewer)) throw new ViewException("viewer not found");
        _viewers.Remove(viewer);
        OnClose.Invoke(new UICloseEventData(this, viewer));
    }

    [EventHandler]
    private void OnPlayerOpenView(UIOpenEventData e)
    {
        if (e.View != this) return;
        _viewers.Add(e.Viewer);
        for (var i = 0; i < Components.Length; i++)
            OnRemoteUpdate.Invoke(new ComponentRemoteUpdateEventData(Components[i]));
    }

    [EventHandler]
    private void OnPlayerDisconnect(PlayerDisconnectedEventData e)
    {
        if (!_viewers.Contains(e.Player)) return;
        _viewers.Remove(e.Player);
    }
    public override void Destroy()
    {
        for (var i = 0; i < _viewers.Count; i++)
        {
            OnClose.Invoke(new UICloseEventData(this, _viewers[i]));
        }
    }

    public override bool ContainsViewer(ISceneViewer viewer) => _viewers.Contains(viewer);
}