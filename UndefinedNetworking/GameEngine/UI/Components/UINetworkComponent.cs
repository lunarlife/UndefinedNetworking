using System;
using Networking;
using Networking.DataConvert;

namespace UndefinedNetworking.GameEngine.UI.Components;

public abstract record UINetworkComponent : UIComponent, INetworkComponent
{
    [ClientData]private Identifier _identifier;

    [ExcludeData]
    public Identifier Identifier
    {
        get => _identifier;
        private set => _identifier = value;
    }

    internal UINetworkComponent()
    {
        Identifier = new Identifier();
    }
}

