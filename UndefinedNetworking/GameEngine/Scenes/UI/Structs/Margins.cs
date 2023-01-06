namespace UndefinedNetworking.GameEngine.Scenes.UI.Structs
{
    public struct Margins
    {
        public static readonly Margins Zero = new()
        {
            Bottom = 0,
            Left = 0,
            Right = 0,
            Top = 0
        };
        public float Left { get; set; }
        public float Right { get; set; }
        public float Top { get; set; }
        public float Bottom { get; set; }
    }
}