using Utils.Events;

namespace UndefinedNetworking.Events.GameEngine;

public class TickEventData : EventData
{
    public float DeltaTime { get; }
    
    public TickEventData(float deltaTime)
    {
        DeltaTime = deltaTime;
    }
}