using System.Collections.Generic;
using UndefinedServer.Exeptions;
using UndefinedServer.GameEngine;
using UndefinedServer.UI.Elements;

namespace UndefinedServer.UI.Windows;

public abstract class Window : UIElement
{
    private readonly List<UIElement> _elements = new();
    public ColoredSprite ContentSprite { get; }
    public ColoredSprite MenuSprite { get; }
    public IReadOnlyList<UIElement> Elements => _elements;
    
    public Window(ColoredSprite? contentSprite = null, ColoredSprite? menuSprite = null, UIElement? parent = null) : base(parent)
    {
        ContentSprite = contentSprite ?? default;
        MenuSprite = menuSprite ?? default;
    }
    public void AddElement(UIElement element)
    {
        if (_elements.Contains(element))
            throw new WindowException("element is already added to window");
        _elements.Add(element);
    }
        
    public void RemoveElement(UIElement element)
    {
        if (!_elements.Contains(element))
            throw new WindowException("window not contains this element");
        _elements.Remove(element);
    }
}