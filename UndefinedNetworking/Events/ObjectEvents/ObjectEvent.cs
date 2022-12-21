using UndefinedNetworking.GameEngine;
using Utils.Events;

namespace UndefinedNetworking.Events.ObjectEvents;

public abstract class ObjectEvent: Event
{
    public abstract IObjectBase Object { get; }
}