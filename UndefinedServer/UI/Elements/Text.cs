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
    private FontStyle _fontStyle;
    private TextWrapping _wraping;
    private FontSize _size;

    public Text(string text, Color? color = null, FontSize? size = null, FontStyle fontStyle = FontStyle.Normal, TextWrapping? textWrapping = null, UIElement? parent = null) : base(parent)
    {
        _color = color ?? Color.Black;
        _text = text;
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
        OriginalRect = new Rect(100, 100, 100, 100)
    };

    public override void OnCreateView(IUIView view)
    {
        var text = view.AddComponent<TextComponent>();
        var mouseUp = view.AddComponent<MouseUpHandlerComponent>();
        var mouseEnter = view.AddComponent<MouseEnterHandlerComponent>();
        var mouseExit = view.AddComponent<MouseExitHandlerComponent>();
        var mouseDown = view.AddComponent<MouseDownHandlerComponent>();
        var mouseHolding = view.AddComponent<MouseHoldingHandlerComponent>();
        mouseUp.Keys = MouseKey.Left;
        mouseHolding.Keys = MouseKey.Right;
        mouseDown.Keys = MouseKey.Middle;
        text.Color = _color;
        text.Text = _text;
        text.FontStyle = _fontStyle;
        text.Wrapping = _wraping;
        text.Size = _size;
        text.Text = _text;
    }
}