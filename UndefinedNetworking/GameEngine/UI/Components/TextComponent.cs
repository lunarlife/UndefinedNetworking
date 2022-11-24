using Networking;
using Networking.DataConvert.DataUse;
using UndefinedNetworking.Events;
using UndefinedNetworking.GameEngine.UI.Elements.Enums;
using UndefinedNetworking.GameEngine.UI.Elements.Structs;
using Utils;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.UI.Components;

[DataConvertUse(DataType.Field)]
public record TextComponent : UINetworkComponent, IEventCaller<TextChangeEvent>
{
    [ClientData] private FontStyle _fontStyle;
    [ClientData] private Color _color;
    [ClientData] private TextWrapping _textWrapping;
    [ClientData] private string _text;



    public FontStyle FontStyle
    {
        get => _fontStyle;
        set
        {
            _fontStyle = value;
            SetNewValuesAndUpdate();
        }
    }

    public Color Color
    {
        get => _color;
        set
        {
            _color = value;
            SetNewValuesAndUpdate();
        }
    }

    public TextWrapping TextWrapping
    {
        get => _textWrapping;
        set
        {
            _textWrapping = value;
            SetNewValuesAndUpdate();
        }   
    }

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            SetNewValuesAndUpdate();
        }
    }
    

    private void SetNewValuesAndUpdate()
    {
        
    }
}