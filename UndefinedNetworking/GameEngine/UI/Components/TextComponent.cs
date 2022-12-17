using Networking.DataConvert;
using Networking.DataConvert.DataUse;
using UndefinedNetworking.Events;
using UndefinedNetworking.GameEngine.UI.Elements.Enums;
using UndefinedNetworking.GameEngine.UI.Elements.Structs;
using Utils;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.UI.Components;

public record TextComponent : UINetworkComponent, IEventCaller<TextChangeEvent>
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
        }
    }
    
    [ClientData] 
    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
        }
    }
    
    [ClientData] 
    public TextWrapping Wrapping
    {
        get => _wrapping;
        set
        {
            _wrapping = value;
        }   
    }
    
    [ClientData] 
    public string Text
    {
        get => _text;
        set
        {
            _text = value;
        }
    }
    
    [ClientData] 
    public FontSize Size
    {
        get => _fontSize;
        set
        {
            _fontSize = value;
        }
    }

    public TextComponent()
    {
        
    }
}