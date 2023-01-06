using UndefinedNetworking.Exceptions;
using UndefinedNetworking.GameEngine.Resources;
using Utils;

namespace UndefinedNetworking.GameEngine;

public abstract class ServerManagerBase
{
    public readonly Version ServerVersion = new("0.1alpha");
    internal static ServerManagerBase ServerManager { get; private set; }
    public abstract IResourcesManager ResourcesManager { get; }

    public ServerManagerBase() => ServerManager = ServerManager is null ? this : throw new ServerManagerException("is a singleton");
}