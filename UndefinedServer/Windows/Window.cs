using System.Collections.Generic;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.GameEngine.UI.Elements;
using UndefinedNetworking.GameEngine.UI.Elements.Interfaces;
using UndefinedNetworking.GameEngine.UI.Windows.Interfaces;

namespace UndefinedServer.Windows;

public class Window : IWindow
{
    private List<IUIElement> _elements;
    public IRectTransform Transform { get; }

    public IReadOnlyList<IUIElement> Elements => _elements;

    public void AddElement(IUIElement element)
    {
        _elements.Add(element);
    }

    public void Remove(IUIElement element)
    {
        _elements.Add(element);
    }

}