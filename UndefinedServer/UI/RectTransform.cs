using System;
using System.Collections.Generic;
using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.GameEngine.UI.Elements.Structs;
using UndefinedServer.Exeptions;
using UndefinedServer.UI.View;
using Utils;
using Utils.Dots;

namespace UndefinedServer.UI;

public class RectTransform : IRectTransform
{
    private Rect _originalRect;
    private Rect _anchoredRect;
    private readonly List<RectTransform> _childs = new();
    private Side _pivotSide;
    private Margins _margins;
    private Dot2Int _bindMultiplier = Dot2Int.Zero;
    private bool _isActive;
    private UIBind _bind;
    private RectTransform? _parent;
    public Rect OriginalRect
    {
        get => _originalRect;
        set
        {
            _originalRect = value;
            UpdateRect();
        }
    }

    public Margins Margins
    {
        get => _margins;
        set
        {
            _margins = value;
            UpdateRect();
        }
    }

    public Rect AnchoredRect => _anchoredRect;

    public bool IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }

    public IRectTransform? Parent
    {
        get => _parent;
        set
        {
            if (value is not null)
                if (value == this)
                    throw new ObjectException("parent cant be this object");
            _parent?._childs.Remove(this);
            _parent = value as RectTransform;
            _parent?._childs.Add(this);
            UpdateRect();
        }
    }
    public int Layer { get; set; }

    public IReadOnlyList<IRectTransform> Childs => _childs;

    public Side Pivot
    {
        get => _pivotSide;
        set { _pivotSide = value; }
    }

    public UIBind Bind
    {
        get => _bind;
        set
        {
            _bind = value;
            UpdateRect();
        }
    }

    public IUIView TargetView { get; }

    private static Dot2Int GetPositionWithBind(Rect rect, RectTransform? parent, Side bind) => parent is null
        ? rect.Position
        : bind switch
        {
            Side.TopLeft => new Dot2Int(rect.Position.X, parent._anchoredRect.Height - rect.Position.Y - rect.Height),
            Side.Top => new Dot2Int(rect.Position.X - rect.Width / 2 + parent._anchoredRect.Width / 2,
                parent._anchoredRect.Height - rect.Position.Y - rect.Height),
            Side.TopRight => new Dot2Int(parent._anchoredRect.Width - rect.Position.X - rect.Width,
                parent._anchoredRect.Height - rect.Position.Y - rect.Height),
            Side.Right => new Dot2Int(parent._anchoredRect.Width - rect.Position.X - rect.Width,
                rect.Position.Y - rect.Height / 2 + parent._anchoredRect.Height / 2),
            Side.RightBottom => new Dot2Int(parent._anchoredRect.Width - rect.Position.X - rect.Width,
                rect.Position.Y),
            Side.Bottom => new Dot2Int(rect.Position.X - rect.Width / 2 + parent._anchoredRect.Width / 2,
                rect.Position.Y),
            Side.LeftBottom => rect.Position,
            Side.Left => new Dot2Int(rect.Position.X,
                rect.Position.Y - rect.Height / 2 + parent._anchoredRect.Height / 2),
            Side.Center => new Dot2Int(rect.Position.X - rect.Width / 2 + parent._anchoredRect.Width / 2,
                rect.Position.Y - rect.Height / 2 + parent._anchoredRect.Height / 2),
            _ => throw new ArgumentOutOfRangeException()
        };

    private void UpdateRect()
    {
        _anchoredRect = CalculateAnchoredRect(_originalRect, uiBind: _bind, parent: _parent);
        for (var i = 0; i < _childs.Count; i++)
        {
            _childs[i].UpdateRect();
        }
    }
    

    internal RectTransform(UIView current, IRectTransform? parent, bool isActive, int layer, Margins margins,
        Rect originalRect, Side pivot, UIBind bind)
    {
        Parent = parent;
        TargetView = current;
        _margins = margins;
        _originalRect = originalRect;
        _pivotSide = pivot;
        IsActive = isActive;
        Layer = layer;
        _bind = bind;
        UpdateRect();
    }

    public static Rect CalculateAnchoredRect(Rect rect, Margins margins = new(), UIBind? uiBind = null, RectTransform? parent = null)
    {
        var bind = uiBind?.Side ?? Side.LeftBottom;
        if (parent is not null && (uiBind?.IsExpandable ?? false))
        {
            var localPosition = (Dot2)rect.Position;
            localPosition.X += margins.Left;
            localPosition.Y += margins.Bottom;
            var wh = (Dot2)rect.WidthHeight;
            wh.X += margins.Right + margins.Left;
            wh.Y += margins.Top + margins.Bottom;
            switch (bind)
            {
                case Side.TopLeft:
                case Side.TopRight:
                case Side.RightBottom:
                case Side.Center:
                case Side.LeftBottom:
                    wh = (Dot2)parent._anchoredRect.WidthHeight - wh;
                    break;
                case Side.Top:
                    localPosition.Y = parent._anchoredRect.Height - localPosition.Y - wh.Y;
                    wh.X = parent._anchoredRect.Width - wh.X;
                    break;
                case Side.Right:
                    localPosition.X = parent._anchoredRect.Width - localPosition.X - wh.X;
                    wh.Y = parent._anchoredRect.Height - wh.Y;
                    break;
                case Side.Bottom:
                    wh.X = parent._anchoredRect.Width - wh.X;
                    break;
                case Side.Left:
                    wh.Y = parent._anchoredRect.Height - wh.Y;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var anchoredPosition = (Dot2)parent.AnchoredRect.Position + localPosition;
            return new Rect(anchoredPosition, wh);
        }
        else
        {
            var localPosition = (Dot2)rect.Position;
            localPosition.X += margins.Left;
            localPosition.Y += margins.Bottom;
            var wh = new Dot2(rect.Width, rect.Height);
            wh.X -= margins.Right - margins.Left;
            wh.Y -= margins.Top - margins.Bottom;
            var localRect = new Rect(localPosition, wh);
            var withBind = (Dot2)GetPositionWithBind(localRect, parent, bind);
            var anchoredPosition = parent is null ? withBind : (Dot2)parent.AnchoredRect.Position + withBind;
            return new Rect(anchoredPosition, wh);
        }
    }
}
