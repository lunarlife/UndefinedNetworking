using System.Collections.Generic;
using UndefinedNetworking.GameEngine.UI.Components;

namespace UndefinedNetworking.GameEngine.UI;

public interface IUIElement
{
    public IUIElement? Parent { get; }
    public IEnumerable<IUIElement> Childs { get; }
    public IEnumerable<UIComponent> Components { get; }
    public ViewParameters CreateNewView(IUIViewer viewer);

}