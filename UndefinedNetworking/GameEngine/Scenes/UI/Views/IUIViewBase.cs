using UndefinedNetworking.Events.UIEvents;
using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Scenes.UI.Components;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Views;

public interface IUIViewBase : IObjectBase, IComponentable<UIComponentData>
{
    public Event<UICloseEventData> OnClose { get; }
    public IComponent<RectTransform> Transform { get; }
    public void Close();
    public bool ContainsViewer(ISceneViewer viewer);
}