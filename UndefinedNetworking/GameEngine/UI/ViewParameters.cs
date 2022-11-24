using UndefinedNetworking.GameEngine.UI.Elements.Structs;
using Utils;

namespace UndefinedNetworking.GameEngine.UI;

public struct ViewParameters
{
    public Margins Margins { get; set; }
    public Rect OriginalRect { get; set; }
    public Side Pivot { get; set; }
    public bool IsActive { get; set; }
    public UIBind Bind { get; set; }
    public int Layer { get; set; }
}