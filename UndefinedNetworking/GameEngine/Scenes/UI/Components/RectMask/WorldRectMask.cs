using Utils;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components.RectMask;

public sealed record WorldRectMask : RectMask
{
    [ClientData]
    public override Rect ViewRect { get; set; }
}