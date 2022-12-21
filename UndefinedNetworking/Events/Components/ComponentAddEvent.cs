using UndefinedNetworking.GameEngine;

namespace UndefinedNetworking.Events.Components;

public class ComponentAddEvent : ComponentEvent
{
    public override Component Component { get; }
    
    public ComponentAddEvent(Component component)
    {
        Component = component;
    }
}