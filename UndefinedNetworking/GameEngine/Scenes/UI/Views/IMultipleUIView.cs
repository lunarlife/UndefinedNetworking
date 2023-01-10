using System.Collections.Generic;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Views;

public interface IMultipleUIView : IUIViewBase
{
    public IEnumerable<ISceneViewer> Viewers { get; }
    public void Close(ISceneViewer viewer);
}