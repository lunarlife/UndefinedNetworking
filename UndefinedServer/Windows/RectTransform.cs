using System.Collections.Generic;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.GameEngine.UI.Elements;
using UndefinedNetworking.GameEngine.UI.Elements.Interfaces;
using UndefinedNetworking.GameEngine.UI.Windows.Interfaces;
using UndefinedServer.Exeptions;
using Utils;

namespace UndefinedServer.Windows;

public sealed class RectTransform : IRectTransform
{
    private readonly List<RectTransform> _childs = new();
    private RectTransform? _parent;
    public Rect Rect { get; set; }
    public UIBind Bind { get; set; }
    public bool IsActive { get; set; }

    public IRectTransform? Parent
    {
        get => _parent;
        set
        {
            if (value is not null && value as RectTransform is not { } )
                throw new ObjectException($"unknown {nameof(RectTransform)}");
            _parent?._childs.Remove(this);
            _parent = value as RectTransform;
            _parent?._childs.Add(this);
        }
    }

    public IUIElement Element { get; }
    public IReadOnlyList<IRectTransform> Childs => _childs;

    public RectTransform(Rect rect, UIBind bind, bool isActive, IRectTransform? parent, IUIElement element)
    {
        Rect = rect;
        Bind = bind;
        IsActive = isActive;
        Parent = parent;
        Element = element;
    }
}