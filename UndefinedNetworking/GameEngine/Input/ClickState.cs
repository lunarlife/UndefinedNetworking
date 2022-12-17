using System;

namespace UndefinedNetworking.GameEngine.Input;

[Flags]
public enum ClickState
{
    Pressed = 1 << 0,
    Double = 1 << 1,
    Down = 1 << 2,
    Up = 1 << 3,
    All = Pressed | Double | Down | Up
}
