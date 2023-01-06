namespace UndefinedNetworking.GameEngine;

public interface IObjectBase
{
    public uint Identifier { get; }
    public void Destroy();
}