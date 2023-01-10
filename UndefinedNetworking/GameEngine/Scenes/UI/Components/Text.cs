using UndefinedNetworking.GameEngine.Scenes.UI.Enums;
using UndefinedNetworking.GameEngine.Scenes.UI.Structs;
using Utils;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components;

public sealed record Text : UINetworkComponentData
{
    [ClientData]
    public FontStyle FontStyle { get; set; }

    [ClientData]
    public Color Color { get; set; }

    [ClientData]
    public TextWrapping Wrapping { get; set; }

    [ClientData]
    public string Content { get; set; }

    [ClientData]
    public FontSize Size { get; set; }
}