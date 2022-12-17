using UndefinedNetworking.GameEngine.UI.Elements.Enums;

namespace UndefinedNetworking.GameEngine.UI.Elements.Structs
{
    public struct TextWrapping
    {
        public TextWrapping()
        {
        }

        public TextOverflow Overflow { get; set; } = TextOverflow.Overflow;
        public bool IsWrapping { get; set; } = false;
        public TextAlignment Alignment { get; set; } = TextAlignment.TopLeft;

    }
}