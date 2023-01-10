using System;
using System.Collections.Generic;
using Networking.DataConvert;
using Networking.DataConvert.Handlers;
using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Scenes.UI.Structs;
using Utils;
using Utils.Dots;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components;

public sealed record RectTransform : UINetworkComponentData, IDeserializeHandler
{
    [ExcludeData] private Rect _anchoredRect;
    [ExcludeData] private IComponent<RectTransform>? _parent;
    [ExcludeData] private readonly List<RectTransform> _childs = new();
    [ExcludeData] private Dot2 _pivotValue;
    [ExcludeData] private Rect _localRect;
    
    [ClientData] private Rect _originalRect;
    [ClientData] private bool _isActiveUi;
    [ClientData] private Margins _margins;
    [ClientData] private int _layer;
    [ClientData] private Side _pivot;
    [ClientData] private UIBind _bind;
    private uint? _parentViewIdentifier;
    
    public void OnDeserialize()
    {
        UpdateRect();
    }


    [ExcludeData] private uint? ParentIdentifier
    {
        get => _parentViewIdentifier;
        set
        {
            _parentViewIdentifier = value;
            UpdateRect();
        }
    }

    [ExcludeData] public Rect OriginalRect
    {
        get => _originalRect;
        set
        {
            _originalRect = value;
            UpdateRect();
        }
    }

    [ExcludeData] public Margins Margins
    {
        get => _margins;
        set
        {
            _margins = value;
            UpdateRect();
        }
    }

    [ExcludeData] public Rect AnchoredRect => _anchoredRect;

    [ExcludeData] public bool IsActiveUI
    {
        get => _isActiveUi;
        set => _isActiveUi = value;
    }

    [ExcludeData]
    public IComponent<RectTransform>? Parent
    {
        get => _parent;
        set
        {
            _parent?.Modify(c => c._childs.Remove(this));
            _parent = value;
            _parent?.Modify(c =>
            {
                c._childs.Add(this);
                _parentViewIdentifier = c.TargetView.Identifier;
            });
            UpdateRect();
        }
    }

    [ExcludeData] public IReadOnlyList<RectTransform> Childs => _childs;

    [ExcludeData] public int Layer
    {
        get => _layer;
        set
        {
            _layer = value;
            UpdateRect();
        }
    }

    [ExcludeData] public Side Pivot
    {
        get => _pivot;
        set
        {
            _pivot = value;
            UpdateRect();
        }
    }

    [ExcludeData] public UIBind Bind
    {
        get => _bind;
        set
        {
            _bind = value;
            UpdateRect();
        }
    }

    [ExcludeData] public Dot2 PivotValue => _pivotValue;

    [ExcludeData] public Rect LocalRect => _localRect;


    private void UpdateRect()
    {
        if (_parentViewIdentifier is { } id)
        {
            if (Parent is null)
            {
                _parent = TargetView.Viewer.ActiveScene.GetView(id).GetComponent<RectTransform>();
                _parent.Modify(transform => transform._childs.Add(this));
            }
            else
            {
                _parent?.Modify(current =>
                {
                    if (current.TargetView.Identifier == _parentViewIdentifier) return;
                    current._childs.Remove(this);
                    _parent = TargetView.Viewer.ActiveScene.GetView(id).GetComponent<RectTransform>();
                    _parent.Modify(transform => transform._childs.Add(this));
                });
            }
        }


        UpdatePivot();
        if (_parent is not null && Bind.IsExpandable)
        {
            var localPosition = (Dot2)_originalRect.Position;
            localPosition.X += _margins.Left;
            localPosition.Y += _margins.Bottom;
            var wh = (Dot2)_originalRect.WidthHeight;
            wh.X += _margins.Right + _margins.Left;
            wh.Y += _margins.Top + _margins.Bottom;
            _parent.Read(transform =>
            {
                switch (Bind.Side)
                {
                    case Side.TopLeft:
                    case Side.TopRight:
                    case Side.RightBottom:
                    case Side.Center:
                    case Side.LeftBottom:
                        wh = (Dot2)transform._anchoredRect.WidthHeight - wh;
                        break;
                    case Side.Top:
                        localPosition.Y = transform._anchoredRect.Height - localPosition.Y - wh.Y;
                        wh.X = transform._anchoredRect.Width - wh.X;
                        break;
                    case Side.Right:
                        localPosition.X = transform._anchoredRect.Width - localPosition.X - wh.X;
                        wh.Y = transform._anchoredRect.Height - wh.Y;
                        break;
                    case Side.Bottom:
                        wh.X = transform._anchoredRect.Width - wh.X;
                        break;
                    case Side.Left:
                        wh.Y = transform._anchoredRect.Height - wh.Y;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var anchoredPosition = (Dot2)transform.AnchoredRect.Position + localPosition;
                _anchoredRect = new Rect(anchoredPosition, wh);
                _localRect = new Rect(localPosition + wh * _pivotValue, wh);
            });
        }
        else
        {
            var localPosition = (Dot2)_originalRect.Position;
            localPosition.X += _margins.Left;
            localPosition.Y += _margins.Bottom;
            var wh = new Dot2(_originalRect.Width, _originalRect.Height);
            wh.X -= _margins.Right - _margins.Left;
            wh.Y -= _margins.Top - _margins.Bottom;
            var localRect = new Rect(localPosition, wh);
            var anchoredRect = Rect.Zero;
            _parent?.Read(transform =>
            {
                anchoredRect = transform.AnchoredRect;
            });
            var withBind = (Dot2)GetPositionWithBind(localRect, anchoredRect);
            var anchoredPosition = _parent is null ? withBind : (Dot2)anchoredRect.Position + withBind;
            _anchoredRect = new Rect(anchoredPosition, wh);
            _localRect = new Rect(withBind + (Dot2)_originalRect.WidthHeight * _pivotValue, wh);
        }

        for (var i = 0; i < _childs.Count; i++)
        {
            _childs[i].UpdateRect();
        }
    }

    private Dot2Int GetPositionWithBind(Rect rect, Rect parentAnchoredRect) => _parent is null
        ? rect.Position
        : Bind.Side switch
        {
            Side.TopLeft => new Dot2Int(rect.Position.X, parentAnchoredRect.Height - rect.Position.Y - rect.Height),
            Side.Top => new Dot2Int(rect.Position.X - rect.Width / 2 + parentAnchoredRect.Width / 2,
                parentAnchoredRect.Height - rect.Position.Y - rect.Height),
            Side.TopRight => new Dot2Int(parentAnchoredRect.Width - rect.Position.X - rect.Width,
                parentAnchoredRect.Height - rect.Position.Y - rect.Height),
            Side.Right => new Dot2Int(parentAnchoredRect.Width - rect.Position.X - rect.Width,
                rect.Position.Y - rect.Height / 2 + parentAnchoredRect.Height / 2),
            Side.RightBottom => new Dot2Int(parentAnchoredRect.Width - rect.Position.X - rect.Width,
                rect.Position.Y),
            Side.Bottom => new Dot2Int(rect.Position.X - rect.Width / 2 + parentAnchoredRect.Width / 2,
                rect.Position.Y),
            Side.LeftBottom => rect.Position,
            Side.Left => new Dot2Int(rect.Position.X,
                rect.Position.Y - rect.Height / 2 + parentAnchoredRect.Height / 2),
            Side.Center => new Dot2Int(rect.Position.X - rect.Width / 2 + parentAnchoredRect.Width / 2,
                rect.Position.Y - rect.Height / 2 + parentAnchoredRect.Height / 2),
            _ => throw new ArgumentOutOfRangeException()
        };

    private void UpdatePivot()
    {
        _pivotValue = _pivot switch
        {
            Side.Left => new Dot2(0, .5f),
            Side.Right => new Dot2(1, .5f),
            Side.Bottom => new Dot2(.5f, 0),
            Side.Top => new Dot2(.5f, 1),
            Side.Center => new Dot2(.5f, .5f),
            Side.TopLeft => new Dot2(0, 1),
            Side.TopRight => new Dot2(1, 1),
            Side.RightBottom => new Dot2(1, 0),
            Side.LeftBottom => new Dot2(0, 0),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    public void ApplyParameters(ViewParameters parameters)
    {
        _originalRect = parameters.OriginalRect;
        _margins = parameters.Margins;
        _bind = parameters.Bind;
        _parent = parameters.Parent;
        _parent?.Modify(transform =>
        {
            _parentViewIdentifier = transform.TargetView.Identifier;
            transform._childs.Add(this);
        });
        _pivot = parameters.Pivot;
        _layer = parameters.Layer;
        _isActiveUi = parameters.IsActive;
        UpdateRect();
    }
    public void SetBind(Side side, bool isExpandable = false)
    {
        Bind = new UIBind
        {
            Side = side,
            IsExpandable = isExpandable
        };
    }
}