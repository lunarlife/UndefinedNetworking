using Utils.Events;

namespace UndefinedNetworking.Events.GameEngine;

public class TickEvent : Event
{
    public float DeltaTime { get; }
    
    public TickEvent(float deltaTime)
    {
        DeltaTime = deltaTime;
    }
}