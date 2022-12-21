using Networking;

namespace UndefinedNetworking.GameEngine;

public interface IObjectBase
{
    public Identifier Identifier { get; }
    public void Destroy();
}