using System;

namespace UndefinedNetworking.GameEngine.Components;

public record ComponentData : IDisposable
{
    public bool IsActive { get; set; } = true;

    public void Dispose()
    {
    }
}