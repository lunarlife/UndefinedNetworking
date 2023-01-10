using Utils;
using Utils.Dots;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components;

public sealed record Scroll : UINetworkComponentData
{
    /// <summary>
    /// zero if only vertical
    /// </summary>
    [ClientData]
    public float HorizontalScrollSpeed { get; set; }

    /// <summary>
    /// zero if only horizontal
    /// </summary>
    [ClientData]
    public float VerticalScrollSpeed { get; set; }

    [ClientData]
    public Rect ViewRect { get; set; }

    [ServerData]
    public Dot2 Position { get; set; }
}