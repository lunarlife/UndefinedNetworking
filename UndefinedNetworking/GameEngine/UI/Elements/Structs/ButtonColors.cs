using Utils;

namespace UndefinedNetworking.GameEngine.UI.Elements.Structs
{
    public readonly struct ButtonColors
    {
        public Color Color { get; }
        public Color PressedColor { get; }
        public Color HighlightedColor { get; }

        public ButtonColors(Color? color = null, Color? highlightedColor = null, Color? pressedColor = null)
        {
            Color = color ?? Color.White;
            PressedColor = pressedColor ?? Color.DarkGray;
            HighlightedColor = highlightedColor ?? Color.Gray;
        }
    }
}