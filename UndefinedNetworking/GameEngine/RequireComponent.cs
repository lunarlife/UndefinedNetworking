using System;
using System.Linq;
using System.Reflection;
using UECS;
using UndefinedNetworking.Core;
using UndefinedNetworking.Exceptions;

namespace UndefinedNetworking.GameEngine;

[AttributeUsage(AttributeTargets.Class)]
public sealed class RequireComponent : Attribute
{
    public Type[] Components { get; }

    public RequireComponent(params Type[] components)
    {
        Components = components;
    }

    public static void AddRequirements<T>(Component component, IComponentable<T> componentable) where T : Component
    {
        var type = component.GetType();
        foreach (var attribute1 in type.GetCustomAttributes().Where(att => att is RequireComponent))
        {
            var attribute = (RequireComponent)attribute1;
            if (!attribute.Components.All(t => !t.IsAbstract && !t.IsInterface && t.IsSubclassOf(typeof(ComponentBase))))
                throw new RequireException($"one of types is not {nameof(ComponentBase)}");
            foreach(var t in attribute.Components) componentable.AddComponent(t);
        }
    }
}