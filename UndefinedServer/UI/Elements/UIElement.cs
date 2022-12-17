using System.Collections.Generic;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.GameEngine.UI.Components;

namespace UndefinedServer.UI.Elements;

public abstract class UIElement : IUIElement
{
    private readonly List<UIElement> _childs = new();
    public IUIElement? Parent { get; }
    public IEnumerable<IUIElement> Childs => _childs;
    public abstract ViewParameters CreateNewView(IUIViewer viewer);
 
    public UIElement(UIElement? parent)
    {
        Parent = parent;
        parent?._childs.Add(this);
    }
    public virtual void OnCreateView(IUIView view) { }

}