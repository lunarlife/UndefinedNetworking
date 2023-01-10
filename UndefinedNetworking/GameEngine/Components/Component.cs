using System;
using System.Linq;
using UECS;
using UndefinedNetworking.Exceptions;
using UndefinedNetworking.GameEngine.Scenes.UI.Components;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;
using Utils;
using Utils.Enums;

namespace UndefinedNetworking.GameEngine.Components;

public abstract class Component : IComponent
{
    private static bool _isLoaded;
    private static Enum<ComponentId> _componentsIds = new();
    private ComponentData _data;
    private object _dataLock = new();

    public IObjectBase TargetObject
    {
        get
        {
            lock (_dataLock)
                return _data.TargetObject;
        }
    }

    public bool ShouldBeUpdatedRemote
    {
        get;
        set;
    }

    public Type ComponentType { get; }
    
    internal Component(ComponentData data)
    {
        _data = data;
        ComponentType = data.GetType();
        SystemsController.MainController?.Add(this);
    }
    

    public IComponent CastModify<T>(Action<T> action) where T : ComponentData
    {
        InitializeModify(action);
        return this;
    }

    public IComponent CastRead<T>(Action<T> action) where T : ComponentData
    {
        InitializeRead(action);
        return this;
    }

    public ComponentData GetCloneData()
    {
        lock (_dataLock)
            return _data with { };
    }

    public void ModifyLocal(Action<ComponentData> action)
    {
        lock (_dataLock)
        {
            using var data = _data;
            action.Invoke(_data);
            _data = _data with { };
            ((IComponentBase)this).Update();
        }
    }

    public bool DataTypeIs(Type type) => type.IsAssignableFrom(ComponentType);

    public bool DataTypeIs<T1>() => DataTypeIs(typeof(T1));

    protected void InitializeModify(Delegate d)
    {
        lock (_dataLock)
        {
            using var data = _data;
            d.DynamicInvoke(_data);
            _data = _data with { };
            ((IComponentBase)this).Update();
            ShouldBeUpdatedRemote = true;
        }
    }
    protected void InitializeRead(Delegate d)
    {
        lock (_dataLock)
        {
            using var data = _data;
            d.DynamicInvoke(_data with { });
        }
    }

    public static void LoadComponents()
    {
        if (_isLoaded) throw new Exception("");
        var componentType = typeof(INetworkComponentData);
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().OrderBy(a => a.FullName))
            
        foreach (var t in assembly.GetTypes().Where(type => !type.IsAbstract && !type.IsInterface && componentType.IsAssignableFrom(type)).OrderBy(t => t.Name))
            _componentsIds.AddMember(t.Name, new ComponentId(t));
        _isLoaded = true;
    }
    public static Type GetComponentType(ushort id) => _componentsIds.Count <= id ? throw new Exception("unknown id") : _componentsIds[id].Type;
    public static ushort GetComponentId(Type type) => (ushort)_componentsIds[type.Name].ID;
}
public sealed class Component<T> : Component, IComponent<T> where T : ComponentData
{


    private Component(T data) : base(data)
    {
        
    }

    public T CloneData => (T)GetCloneData();

    public IComponent<T> Modify(Action<T> action)
    {
        InitializeModify(action);
        return this;
    }

    public IComponent<T> Read(Action<T> action)
    {
        InitializeRead(action);
        return this;
    }

    public static Component<T1> CreateInstance<T1>() where T1 : ComponentData, new()
    {
        var component = new Component<T1>(new T1());
        return component;
    }      
    public static IComponent<T> CreateInstance(Type dataType)
    {
        if (!typeof(ComponentData).IsAssignableFrom(dataType))
            throw new ComponentException($"type is not {nameof(ComponentData)}");
        if (ReflectionUtils.GetEmptyConstructor(dataType) is not { } constructor)
            throw new ComponentException("data has no empty constructor");
        var componentType = typeof(Component<>).MakeGenericType(dataType);
        var ctor = ReflectionUtils.GetConstructor(componentType, dataType)!;
        return (IComponent<T>)ctor.Invoke(new[] { constructor.Invoke(Array.Empty<object>()) });
    }
}
