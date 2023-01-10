using System;

namespace UndefinedNetworking.GameEngine;

public enum ServerType
{
    Server,
    Client 
}

internal static class ServerTypeExtensions
{
    public static ServerType GetRemoteType(this ServerType type) => type switch
    {
        ServerType.Server => ServerType.Client,
        ServerType.Client => ServerType.Server,
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };
} 