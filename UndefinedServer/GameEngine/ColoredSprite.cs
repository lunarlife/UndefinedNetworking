using Utils;

namespace UndefinedServer.GameEngine;

public struct ColoredSprite
{
    public ColoredSprite()
    {
        Sprite = null;
        Color = Color.Black;
    }

    public Sprite? Sprite { get; set; }
    public Color Color { get; set; }
}