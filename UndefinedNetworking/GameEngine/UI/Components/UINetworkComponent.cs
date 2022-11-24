using Networking;
using Networking.DataConvert;
using UndefinedNetworking.Exceptions;

namespace UndefinedNetworking.GameEngine.UI.Components;

public abstract record UINetworkComponent : UIComponent, INetworkComponent
{
    [ExcludeData] public Identifier NetIdentifier { get; }
    [ExcludeData] public bool IsNetInitialized { get; private set; }

    [ExcludeData] bool INetworkComponent.IsChanged { get; set; }

    internal UINetworkComponent()
    {
        NetIdentifier = new Identifier();
    }
    
    public void Initialize()
    {
        if (IsNetInitialized) throw new NetComponentInitializeException("component already initialized");
        IsNetInitialized = true;
    }

}