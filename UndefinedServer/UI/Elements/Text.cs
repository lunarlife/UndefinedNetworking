using System.Collections.Generic;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.GameEngine.UI.Components;
using UndefinedNetworking.GameEngine.UI.Elements.Enums;
using UndefinedNetworking.GameEngine.UI.Elements.Structs;
using Utils;

namespace UndefinedServer.UI.Elements;

public class Text : UIElement
{
    private UIComponent[] _components;

    private Color _color;
    private string _text;
    private FontStyle _fontStyle;
    private TextWrapping _textWraping;

    public Text(string text, Color? color = null, FontStyle fontStyle = FontStyle.Normal, TextWrapping? textWraping = null, UIElement? parent = null) : base(parent)
    {
        _color = color ?? Color.Black;
        _text = text;
        _fontStyle = fontStyle;
        _textWraping = textWraping ?? new TextWrapping
        {
            Alignment = TextAlignment.TopLeft,
            Overflow = TextOverflow.Overflow,
            IsWrapping = true
        };
        _components = new UIComponent[]
        {
            new TextComponent
            {
                Color = _color,
                Text = _text,
                FontStyle = _fontStyle,
                TextWrapping = _textWraping
            }
        };
    }

    public override IEnumerable<UIComponent> Components => _components;

    public override ViewParameters CreateNewView(IUIViewer viewer) => new()
    {
        
    };
}