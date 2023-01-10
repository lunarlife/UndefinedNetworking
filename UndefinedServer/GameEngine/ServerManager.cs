using Networking.Loggers;
using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.Resources;
using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;
using UndefinedServer.GameEngine.Resources;
using UndefinedServer.UI.View;

namespace UndefinedServer.GameEngine;

public sealed class ServerManager : ServerManagerBase
{
    protected override ServerType _ServerType => ServerType.Server;
    public override IResourcesManager ResourcesManager { get; }
    public override IUIViewBase GetView(uint id) => UIView.GetView(id);

    public ServerManager(Logger logger) : base(logger)
    {
        ResourcesManager = new ResourcesManager();
    }
}