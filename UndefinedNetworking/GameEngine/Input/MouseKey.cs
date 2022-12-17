using System;

namespace UndefinedNetworking.GameEngine.Input;

[Flags]
public enum MouseKey
{
    Left = 1 << 0,
    Right = 1 << 1,
    Middle = 1 << 2
}