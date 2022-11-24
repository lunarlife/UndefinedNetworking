using Networking;

namespace UndefinedNetworking.GameEngine.UI.Components;

public interface INetworkComponent
{
    public Identifier NetIdentifier { get; }
    public bool IsNetInitialized { get; }
    public bool IsChanged { get; protected set; }
    public void Initialize();
}