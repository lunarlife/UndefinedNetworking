using UndefinedNetworking.GameEngine;
using Utils.Events;

namespace UndefinedNetworking.Events.ObjectEvents;

public abstract class ObjectEventData : EventData
{
    public abstract IObjectBase Object { get; }
}