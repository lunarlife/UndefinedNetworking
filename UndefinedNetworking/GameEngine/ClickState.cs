using System;

namespace UndefinedNetworking.GameEngine;

[Flags]
public enum ClickState : byte
{
    Pressed = 0,
    Double = 1 << 0,
    Down = 1 << 1,
    Up = 1 << 2,
    All = Pressed | Double | Down | Up
}
