using System;
using Networking.DataConvert;
using Networking.Loggers;
using Networking.Packets;
using UndefinedNetworking.Converters;
using UndefinedNetworking.Exceptions;
using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Resources;
using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;
using Version = Utils.Version;

namespace UndefinedNetworking.GameEngine;

public abstract class ServerManagerBase
{
    public readonly Version ServerVersion = new("0.1alpha");
    internal static ServerManagerBase ServerManager { get; private set; }
    internal static ServerType Type => ServerManager._ServerType;
    public ServerManagerBase(Logger logger)
    {
        ServerManager = ServerManager is null ? this : throw new ServerManagerException("is a singleton");
        Logger = logger;
        Initialize();
    }

    private static void Initialize()
    {
        AppDomain.CurrentDomain.Load("Utils");
        AppDomain.CurrentDomain.Load("Networking");
        AppDomain.CurrentDomain.Load("UECS");
        AppDomain.CurrentDomain.Load("UndefinedNetworking");
        DataConverter.AddStaticConverter(new ColorConverter());
        DataConverter.AddStaticConverter(new RectConverter());
        DataConverter.AddDynamicConverter(new ResourceConverter());
        DataConverter.AddDynamicConverter(new ComponentConverter());
        ServerManager.Logger.Info("Loading assemblies...");
        Packet.LoadPackets();
        Component.LoadComponents();
    }
    protected abstract ServerType _ServerType { get; }
    public Logger Logger { get; }


    public abstract IResourcesManager ResourcesManager { get; }
    public abstract IUIViewBase GetView(uint id);

}