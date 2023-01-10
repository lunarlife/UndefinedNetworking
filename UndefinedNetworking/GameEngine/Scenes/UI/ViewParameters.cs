using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Scenes.UI.Components;
using UndefinedNetworking.GameEngine.Scenes.UI.Structs;
using Utils;

namespace UndefinedNetworking.GameEngine.Scenes.UI;

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
    public IComponent<RectTransform>? Parent { get; set; }
    public Margins Margins { get; set; }
    public Rect OriginalRect { get; set; }
    public Side Pivot { get; set; }
    public bool IsActive { get; set; }
    public UIBind Bind { get; set; }
    public int Layer { get; set; }
}