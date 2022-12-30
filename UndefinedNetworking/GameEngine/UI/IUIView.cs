using UndefinedNetworking.Core;
using UndefinedNetworking.GameEngine.Scenes;
using UndefinedNetworking.GameEngine.UI.Components;

namespace UndefinedNetworking.GameEngine.UI;

public interface IUIView : IObjectBase, IComponentable<UIComponent>
{
    public RectTransform Transform { get; }
    public ISceneViewer Viewer { get; }
    public void Close();
}