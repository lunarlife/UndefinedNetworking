using System;
using Networking.DataConvert;

namespace UndefinedNetworking.GameEngine.Components;

public record ComponentData : IDisposable
{
    /*public bool IsActive { get; set; } = true;*/
    [ExcludeData] public IObjectBase TargetObject { get; private set; }
    public void Dispose()
    {
    }
}