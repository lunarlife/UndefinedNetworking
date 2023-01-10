using System;
using Utils.Enums;

namespace UndefinedNetworking.GameEngine.Components;

public class ComponentId : EnumType
{
    public Type Type { get; }

    public ComponentId(Type type)
    {
        Type = type;
    }
}