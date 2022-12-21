using System;
using System.Collections.Generic;
using UndefinedNetworking.Core;
using UndefinedNetworking.Exceptions;
using UndefinedNetworking.GameEngine.Objects.Components;
using Utils;

namespace UndefinedServer.GameEngine.Objects;

public class GameObjectAccess3D
{
    private readonly List<GameObject3DComponent> _components = new();

    public IEnumerable<GameObject3DComponent> Components => _components;

    internal GameObjectAccess3D(GameObject3D gameObject)
    {
        
    }
    
    public GameObjectAccess3D AddComponent<T>(Applier<T>? applier = null) where T : GameObject3DComponent
    {
        applier?.Invoke((T)AddComponent(typeof(T)));
        return this;
    }
    
    // ReSharper disable once MethodOverloadWithOptionalParameter
    public GameObjectAccess3D AddComponent(Type type, Applier<GameObject3DComponent>? applier = null)
    {
        if (!type.IsSubclassOf(typeof(GameObject3DComponent)))
            throw new ComponentException($"component should be subclass of {nameof(GameObject3DComponent)}");
        applier?.Invoke(AddComponent(type));
        return this;
    }
    private GameObject3DComponent AddComponent(Type type)
    {
        var component = (GameObject3DComponent)ReflectionUtils
            .GetEmptyConstructor(type)!.Invoke(null);
        _components.Add(component);
        return component;
    }
}