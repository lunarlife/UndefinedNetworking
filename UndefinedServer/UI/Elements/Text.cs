using System.Collections.Generic;
using UndefinedNetworking.GameEngine.Input;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.GameEngine.UI.Components;
using UndefinedNetworking.GameEngine.UI.Components.Mouse;
using UndefinedNetworking.GameEngine.UI.Elements.Enums;
using UndefinedNetworking.GameEngine.UI.Elements.Structs;
using Utils;

namespace UndefinedServer.UI.Elements;

public class Text : UIElement
{
    private Color _color;
    private string _text;
    private readonly Rect _rect;
    private FontStyle _fontStyle;
    private TextWrapping _wraping;
    private FontSize _size;

    public Text(Rect rect, string text, Color? color = null, FontSize? size = null, FontStyle fontStyle = FontStyle.Normal, TextWrapping? textWrapping = null, UIElement? parent = null) : base(parent)
    {
        _color = color ?? Color.Black;
        _text = text;
        _rect = rect;
        _fontStyle = fontStyle;
        _size = size ?? new FontSize(28);
        _wraping = textWrapping ?? new TextWrapping
        {
            Alignment = TextAlignment.TopLeft,
            Overflow = TextOverflow.Overflow,
            IsWrapping = true
        };
    }

    public override ViewParameters CreateNewView(IUIViewer viewer) => new()
    {
        OriginalRect = _rect
    };

    public override void OnCreateView(IUIView view)
    {
        var text = view.AddComponent<TextComponent>();
        text.Color = _color;
        text.Text = _text;
        text.FontStyle = _fontStyle;
        text.Wrapping = _wraping;
        text.Size = _size;
        text.Text = _text;
    }
}