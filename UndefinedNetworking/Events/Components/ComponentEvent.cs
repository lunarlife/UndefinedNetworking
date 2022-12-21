using UndefinedNetworking.GameEngine;
using Utils.Events;

namespace UndefinedNetworking.Events.Components;

public abstract class ComponentEvent : Event
{
    public abstract Component Component { get; }
}