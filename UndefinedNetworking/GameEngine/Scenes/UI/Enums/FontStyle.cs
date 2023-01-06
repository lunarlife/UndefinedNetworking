using System;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Enums;

[Flags]
public enum FontStyle
{
    Normal = 1 << 0,
    Bold = 1 << 1,
    Italic = 1 << 2,
    Underline = 1 << 3,
    LowerCase = 1 << 4,
    UpperCase = 1 << 5,
    SmallCaps = 1 << 6,
    Strikethrough = 1 << 7,
    Superscript = 1 << 8,
    Subscript = 1 << 9,
    Highlight = 1 << 10
}