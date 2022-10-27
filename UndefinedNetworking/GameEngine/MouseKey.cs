using System;
using System.Collections.Generic;
using System.Linq;

namespace UndefinedNetworking.GameEngine;

[Flags]
public enum MouseKey : byte
{
    None = 0,
    Left = 1 << 0,
    Right = 1 << 1,
    Middle = 1 << 2,
}