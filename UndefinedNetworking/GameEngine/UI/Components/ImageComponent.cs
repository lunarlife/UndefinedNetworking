using Networking.DataConvert;
using UndefinedNetworking.Events;
using UndefinedNetworking.GameEngine.UI.Elements.Enums;
using UndefinedNetworking.GameEngine.UI.Elements.Structs;
using Utils;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.UI.Components;

public record ImageComponent : UINetworkComponent
{
    [ExcludeData] private ISprite _sprite;
    [ExcludeData] private Color _color;
    [ExcludeData] private bool _raycastTarget;
    [ExcludeData] private Margins _raycastPadding;
    [ExcludeData] private FilledSettings? _filledSettings;

    [ClientData] 
    public ISprite ISprite
    {
        get => _sprite;
        set
        {
            _sprite = value;
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
    public Margins RaycastPadding
    {
        get => _raycastPadding;
        set
        {
            _raycastPadding = value;
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

    public ImageComponent()
    {
        
    }
}