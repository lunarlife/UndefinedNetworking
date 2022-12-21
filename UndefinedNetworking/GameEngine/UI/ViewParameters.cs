using UndefinedNetworking.GameEngine.UI.Elements.Structs;
using Utils;

namespace UndefinedNetworking.GameEngine.UI;

public struct ViewParameters
{
    public ViewParameters()
    {
        Margins = default;
        OriginalRect = default;
        Pivot = Side.TopLeft;
        Bind = default;
        Layer = 0;
        IsActive = true;
        Parent = null;
    }
    public RectTransformBase? Parent { get; set; }
    public Margins Margins { get; set; }
    public Rect OriginalRect { get; set; }
    public Side Pivot { get; set; }
    public bool IsActive { get; set; }
    public UIBind Bind { get; set; }
    public int Layer { get; set; }
}