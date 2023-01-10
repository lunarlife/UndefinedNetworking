using System;
using Utils.Events;

namespace UndefinedServer.Pings;

public abstract class Ping : IDisposable, IEventListener
{
   
    public abstract int Delay { get; }
    public abstract DateTime LastPingUpdate { get; }
    public abstract int InvalidRequestsCount { get; }

    internal Ping()
    {
        
    }
    public static implicit operator int(Ping ping) => ping.Delay;
    public override string ToString()
    {
        return Delay.ToString();
    }

    public abstract void Dispose();

    public abstract void Update();


}