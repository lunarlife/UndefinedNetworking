using UndefinedNetworking.Core;
using UndefinedNetworking.GameEngine.Scenes.UI.Components;

namespace UndefinedNetworking.GameEngine.Scenes.UI;

public interface IUIView : IObjectBase, IComponentable<UIComponent>
{
    public RectTransform Transform { get; }
    public ISceneViewer Viewer { get; }
    public void Close();
}