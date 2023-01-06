using UndefinedNetworking.GameEngine.Scenes.UI.Enums;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Structs
{
    public struct FilledSettings
    {
        public FillMethod FillMethod { get; set; } 
        public Side90 FillOrigin { get; set; }
        public float FillAmount { get; set; }
    }
}