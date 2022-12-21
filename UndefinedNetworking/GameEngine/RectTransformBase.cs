using System.Collections.Generic;
using UndefinedNetworking.GameEngine.UI.Components;
using UndefinedNetworking.GameEngine.UI.Elements.Structs;
using Utils;

namespace UndefinedNetworking.GameEngine;

public abstract record RectTransformBase : UINetworkComponent
{
    public abstract Rect OriginalRect { get; set; }
    public abstract Margins Margins { get; set; }
    public abstract Rect AnchoredRect { get; }
    public abstract bool IsActive { get; set; }
    public abstract RectTransformBase? Parent { get; set; }
    public abstract IReadOnlyList<RectTransformBase> Childs { get; }
    public abstract int Layer { get; set; }
    public abstract Side Pivot { get; set; }
    public abstract UIBind Bind { get; set; }

    public void SetBind(Side side, bool isExpandable = false)
    {
        Bind = new UIBind
        {
            Side = side,
            IsExpandable = isExpandable
        };
    }
}