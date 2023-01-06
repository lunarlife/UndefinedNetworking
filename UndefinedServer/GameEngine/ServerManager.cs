using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.Resources;
using UndefinedServer.GameEngine.Resources;

namespace UndefinedServer.GameEngine;

public sealed class ServerManager : ServerManagerBase
{
    
    public override IResourcesManager ResourcesManager { get; }
    
    public ServerManager()
    {
        ResourcesManager = new ResourcesManager();
    }
}