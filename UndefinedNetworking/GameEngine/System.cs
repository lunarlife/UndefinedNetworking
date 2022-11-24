using UECS;

namespace UndefinedNetworking.GameEngine;

public abstract class System : ISystem
{
    public virtual void Init() { }
    public virtual void Update() { }
}