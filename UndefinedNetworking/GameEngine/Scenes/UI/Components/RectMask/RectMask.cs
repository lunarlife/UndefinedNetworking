using Utils;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components.RectMask;

public abstract record RectMask : UINetworkComponentData
{
    [ClientData]
    public abstract Rect ViewRect { get; set; }
}