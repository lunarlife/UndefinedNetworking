using UndefinedNetworking.GameEngine.Scenes.UI.Enums;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Structs
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