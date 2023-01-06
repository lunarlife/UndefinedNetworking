using System.Drawing;

namespace UndefinedNetworking.GameEngine.Scenes.UI;

public struct UIColorStates
{
    public Color Normal { get; set; }
    public Color Pressed { get; set; }
    public Color Holding { get; set; }
    public float FadeDuration { get; set; }
}