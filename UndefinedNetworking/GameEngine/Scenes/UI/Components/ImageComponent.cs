using Networking.DataConvert;
using UndefinedNetworking.GameEngine.Resources;
using UndefinedNetworking.GameEngine.Scenes.UI.Structs;
using Utils;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components;

public record ImageComponent : UINetworkComponentData
{
    [ClientData]
    public ISprite? Sprite { get; set; }

    [ExcludeData]
    public IShader? Shader { get; set; }

    [ClientData]
    public Color Color { get; set; }

    [ClientData]
    public bool RaycastTarget { get; set; }

    [ClientData]
    public FilledSettings? FilledSettings { get; set; }
}