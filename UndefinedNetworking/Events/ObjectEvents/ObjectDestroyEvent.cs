using UndefinedNetworking.GameEngine;

namespace UndefinedNetworking.Events.ObjectEvents;

public class ObjectDestroyEvent : ObjectEvent
{
    public ObjectDestroyEvent(IObjectBase o)
    {
        Object = o;
    }

    public override IObjectBase Object { get; }
}