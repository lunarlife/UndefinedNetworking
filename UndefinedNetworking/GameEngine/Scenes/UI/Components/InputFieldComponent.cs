using Networking.DataConvert;
using Networking.DataConvert.Handlers;
using UndefinedNetworking.Events.InputFieldEvents;
using UndefinedNetworking.GameEngine.Scenes.UI.Enums;
using UndefinedNetworking.GameEngine.Scenes.UI.Structs;
using Utils;
using Utils.Dots;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components;

public sealed record InputFieldComponent : UINetworkComponent, IClientChangeHandler, IEventCaller<InputFieldTextChangedEvent>, IDeserializeHandler
{
    private string _text = "";
    [ExcludeData] private FontStyle _fontStyle;
    [ExcludeData] private FontSize _fontSize;
    [ExcludeData] private Color _color;
    [ExcludeData] private TextWrapping _wrapping;
    [ExcludeData] private Dot2 _caretSize;

    [ExcludeData] public string Text
    {
        get => _text;
        set
        {
            _text = value;
            Update();
        }
    }
    
    public void OnDeserialize()
    {
        this.CallEvent(new InputFieldTextChangedEvent(this));
    }
    
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
    public FontSize Size
    {
        get => _fontSize;
        set
        {
            _fontSize = value;
            Update();
        }
    }
    [ClientData]
    public Dot2Int CaretSize
    {
        get => _caretSize;
        set
        {
            _caretSize = value;
            Update();
        }
    }
}