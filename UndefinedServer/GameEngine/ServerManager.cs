using Networking.Loggers;
using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.Resources;
using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedServer.GameEngine.Resources;
using UndefinedServer.Loggers;
using UndefinedServer.UI.View;

namespace UndefinedServer.GameEngine;

public sealed class ServerManager : ServerManagerBase
{
    protected override ServerType _ServerType => ServerType.Server;
    public override IResourcesManager ResourcesManager { get; }
    public override IUIView GetView(uint id) => UIView.GetView(id);

    public ServerManager(Logger logger) : base(logger)
    {
        ResourcesManager = new ResourcesManager();
    }
}