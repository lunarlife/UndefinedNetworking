using Utils;

namespace UndefinedNetworking.GameEngine.UI.Elements.Structs
{
    public readonly struct ButtonColors
    {
        public Color Color { get; }
        public Color PressedColor { get; }
        public float FadeDuration { get; }
        public Color HighlightedColor { get; }

        public ButtonColors(Color? color = null, Color? highlightedColor = null, Color? pressedColor = null, float? fadeDuration = null)
        {
            Color = color ?? Color.White;
            PressedColor = pressedColor ?? Color.DarkGray;
            FadeDuration = fadeDuration ?? .1f;
            HighlightedColor = highlightedColor ?? Color.Gray;
        }
    }
}