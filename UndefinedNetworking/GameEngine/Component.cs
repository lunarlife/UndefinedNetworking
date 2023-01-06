using System;
using System.Linq;
using UECS;
using UndefinedNetworking.GameEngine.Scenes.UI.Components;
using Utils.Enums;

namespace UndefinedNetworking.GameEngine;

public abstract record Component : ComponentBase
{
    private static bool _isLoaded;
    private static Enum<ComponentId> _componentsIds = new();

    public Component()
    {
        SystemsController.MainController?.Add(this);
    }

    protected virtual void Initialize() { }
    
    public static void LoadComponents()
    {
        if (_isLoaded) throw new Exception("");
        var componentType = typeof(INetworkComponent);
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().OrderBy(a => a.FullName))
            
        foreach (var t in assembly.GetTypes().Where(type => !type.IsAbstract && !type.IsInterface && componentType.IsAssignableFrom(type)).OrderBy(t => t.Name))
            _componentsIds.AddMember(t.Name, new ComponentId(t));
        _isLoaded = true;
    }
    public static Type GetComponentType(ushort id) => _componentsIds.Count <= id ? throw new Exception("unknown id") : _componentsIds[id].Type;
    public static ushort GetComponentId(Type type) => (ushort)_componentsIds[type.Name].ID;
}

public class ComponentId : EnumType
{
    public Type Type { get; }

    public ComponentId(Type type)
    {
        Type = type;
    }
}