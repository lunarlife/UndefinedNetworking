using Networking;
using Networking.DataConvert;

namespace UndefinedNetworking.GameEngine.UI.Components;

public abstract record UINetworkComponent : UIComponent, INetworkComponent
{
    [ClientData] private Identifier _identifier;

    [ExcludeData] public Identifier Identifier => _identifier;

    internal UINetworkComponent()
    {
        _identifier = new Identifier();
    }
}

