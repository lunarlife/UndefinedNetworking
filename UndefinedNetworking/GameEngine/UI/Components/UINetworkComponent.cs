using System;
using Networking;
using Networking.DataConvert;

namespace UndefinedNetworking.GameEngine.UI.Components;

public abstract record UINetworkComponent : UIComponent, INetworkComponent
{
    [ClientData] private Identifier _identifier;

    [ExcludeData]
    public Identifier Identifier => _identifier;

    internal UINetworkComponent()
    {
        _identifier = new Identifier();
    }

    public static UINetworkComponent GetUninitializedNetworkComponent(Type type)
    {
        if (!typeof(UINetworkComponent).IsAssignableFrom(type)) return null;
        var instance = (UINetworkComponent)Activator.CreateInstance(type);
        instance._identifier = null;
        return instance;
    }
}

