using System;
using Networking.DataConvert;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.GameEngine.UI.Elements.Structs;
using Utils;

namespace UndefinedNetworking.GameEngine;

public interface IRectTransform
{
    public Rect OriginalRect { get; set; }
    public Margins Margins { get; set; }
    public Rect AnchoredRect { get; }
    public bool IsActive { get; set; }
    public IRectTransform? Parent { get; set; }
    public int Layer { get; set; }
    public Side Pivot { get; set; }
    public IUIView TargetView { get; }
    public UIBind Bind { get; set; }

}