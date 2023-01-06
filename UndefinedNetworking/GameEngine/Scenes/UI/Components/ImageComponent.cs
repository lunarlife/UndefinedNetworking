using Networking.DataConvert;
using UndefinedNetworking.GameEngine.Resources;
using UndefinedNetworking.GameEngine.Scenes.UI.Structs;
using Utils;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components;

public record ImageComponent : UINetworkComponent
{
    [ExcludeData] private ISprite? _sprite;
    [ExcludeData] private Color _color;
    [ExcludeData] private bool _raycastTarget;
    [ExcludeData] private FilledSettings? _filledSettings;
    [ExcludeData] private IShader? _shader;

    [ClientData] 
    public ISprite? Sprite
    {
        get => _sprite;
        set
        {
            _sprite = value;
            Update();
        }
    }

    [ExcludeData]
    public IShader? Shader
    {
        get => _shader;
        set
        {
            _shader = value;
            Update();
        }
    }

    [ClientData] 
    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            Update();
        }
    }
    [ClientData] 
    public bool RaycastTarget
    {
        get => _raycastTarget;
        set
        {
            _raycastTarget = value;
            Update();
        }
    }
    [ClientData] 
    public FilledSettings? FilledSettings
    {
        get => _filledSettings;
        set
        {
            _filledSettings = value;
            Update();
        }
    }
}