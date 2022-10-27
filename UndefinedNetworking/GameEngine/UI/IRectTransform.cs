using System.Collections.Generic;
using UndefinedNetworking.GameEngine.UI.Elements;
using UndefinedNetworking.GameEngine.UI.Elements.Interfaces;
using UndefinedNetworking.GameEngine.UI.Elements.Structs;
using Utils;

namespace UndefinedNetworking.GameEngine.UI;

public interface IRectTransform
{
    public Rect OriginalRect { get; set; }
    public Rect AnchoredRect { get; }
    public Side Bind { get; }
    public Side Pivot { get; set; }
    public Margins Margins { get; set; }
    public bool IsActive { get; set; }
    public IRectTransform? Parent { get; set; }
    public bool IsExpandable { get; }
    public int Layer { get; set; }
    public IUIElement Element { get; }
    public IReadOnlyList<IRectTransform> Childs { get; }
    public void SetBind(UIBind bind);
    public void SetBind(Side bind, bool isExpandable = false) => SetBind(new UIBind { Bind = bind, IsExpandable = isExpandable});
}