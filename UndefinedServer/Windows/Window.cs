using System.Collections.Generic;
using UndefinedNetworking.Window;
using UndefinedNetworking.Window.UI;

namespace UndefinedServer.Windows;

public class Window : IWindow
{
    public IRectTransform Transform { get; }
    private readonly List<IUIElement> _elements = new();

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