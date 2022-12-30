using Utils;

namespace UndefinedNetworking.GameEngine.UI.Components.RectMask;

public abstract record RectMaskComponent : UINetworkComponent
{
    [ClientData]
    public abstract Rect ViewRect { get; set; }
}