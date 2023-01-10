using UndefinedNetworking.Events.UIEvents;
using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Scenes.UI.Components;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.Scenes.UI;

public interface IUIView : IObjectBase, IComponentable<UIComponentData>
{
    public Event<UICloseEventData> OnClose { get; }
    public IComponent<RectTransform> Transform { get; }
    public ISceneViewer Viewer { get; }
    public void Close();
}