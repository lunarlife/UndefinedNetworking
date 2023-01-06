using UndefinedNetworking.Events.GameEngine;

namespace UndefinedNetworking.GameEngine.Jobs;

public abstract class JobInstruction
{
    public abstract bool IsReady { get; }
    public virtual void Tick(TickEvent e) { }
}