using System;
using Networking;
using UECS;

namespace UndefinedNetworking.GameEngine.Components;

public interface IComponent : IComponentBase
{
    public Type ComponentType { get; }
    public IComponent CastModify<T>(Action<T> action) where T : ComponentData;
    public IComponent CastRead<T>(Action<T> action) where T : ComponentData;
    public ComponentData GetCloneData();
    public void ModifyLocal(Action<ComponentData> action);
    public bool DataTypeIs(Type type);
    public bool DataTypeIs<T>();
}
public interface IComponent<out T> : IComponent where T : ComponentData
{
    public T CloneData { get; }
    public IComponent<T> Modify(Action<T> action);
    public IComponent<T> Read(Action<T> action);
}