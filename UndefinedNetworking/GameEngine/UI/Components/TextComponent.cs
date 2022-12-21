using Networking.DataConvert;
using UndefinedNetworking.Events;
using UndefinedNetworking.GameEngine.UI.Elements.Enums;
using UndefinedNetworking.GameEngine.UI.Elements.Structs;
using Utils;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.UI.Components;

public record TextComponent : UINetworkComponent
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