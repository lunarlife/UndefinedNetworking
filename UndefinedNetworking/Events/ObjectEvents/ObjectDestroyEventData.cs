using UndefinedNetworking.GameEngine;

namespace UndefinedNetworking.Events.ObjectEvents;

public class ObjectDestroyEventData : ObjectEventData
{
    public ObjectDestroyEventData(IObjectBase o)
    {
        Object = o;
    }

    public override IObjectBase Object { get; }
}