using Networking.DataConvert;
using UndefinedNetworking.GameEngine.Scenes.UI.Enums;
using UndefinedNetworking.GameEngine.Scenes.UI.Structs;
using Utils;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components;

public sealed record TextComponent : UINetworkComponent
{
    [ExcludeData] private FontStyle _fontStyle;
    [ExcludeData] private FontSize _fontSize;
    [ExcludeData] private Color _color;
    [ExcludeData] private TextWrapping _wrapping;
    [ExcludeData] private string _text;


    [ClientData] 
    public FontStyle FontStyle
    {
        get => _fontStyle;
        set
        {
            _fontStyle = value;
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
    public TextWrapping Wrapping
    {
        get => _wrapping;
        set
        {
            _wrapping = value;
            Update();
        }   
    }
    
    [ClientData] 
    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            Update();
        }
    }
    
    [ClientData] 
    public FontSize Size
    {
        get => _fontSize;
        set
        {
            _fontSize = value;
            Update();
        }
    }

    public TextComponent()
    {
        
    }
}