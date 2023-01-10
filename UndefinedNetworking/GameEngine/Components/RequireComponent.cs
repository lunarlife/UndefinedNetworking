using System;
using System.Linq;
using System.Reflection;
using UndefinedNetworking.Exceptions;

namespace UndefinedNetworking.GameEngine.Components;

[AttributeUsage(AttributeTargets.Class)]
public sealed class RequireComponent : Attribute
{
    public Type[] Components { get; }

    public RequireComponent(params Type[] components)
    {
        Components = components;
    }

    public static void AddRequirements<T>(IComponent<T> component, IComponentable<T> componentable) where T : ComponentData
    {
        component.Modify(componentData =>
        {
            var type = componentData.GetType();
            foreach (var attribute1 in type.GetCustomAttributes().Where(att => att is RequireComponent))
            {
                var attribute = (RequireComponent)attribute1;
                if (!attribute.Components.All(t => !t.IsAbstract && !t.IsInterface && t.IsSubclassOf(typeof(ComponentData))))
                    throw new RequireException($"one of types is not {nameof(ComponentData)}");
                foreach(var t in attribute.Components) componentable.AddComponent(t);
            }
        });
    }
}