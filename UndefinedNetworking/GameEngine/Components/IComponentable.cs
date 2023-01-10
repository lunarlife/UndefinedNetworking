using System;

namespace UndefinedNetworking.GameEngine.Components;

public interface IComponentable<T> where T : ComponentData
{
    public IComponent<T>[] Components { get; }
    public IComponent<T>[] AddComponents<T1, T2, T3, T4, T5>()
        where T1 : T, new()
        where T2 : T, new()
        where T3 : T, new()
        where T4 : T, new()
        where T5 : T, new() => AddComponents(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
    public IComponent<T>[] AddComponents<T1, T2, T3, T4>()
        where T1 : T, new()
        where T2 : T, new()
        where T3 : T, new()
        where T4 : T, new() => AddComponents(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
    public IComponent<T>[] AddComponents<T1, T2, T3>()
        where T1 : T, new()
        where T2 : T, new()
        where T3 : T, new() => AddComponents(typeof(T1), typeof(T2), typeof(T3));
    public IComponent<T>[] AddComponents<T1, T2>()
        where T1 : T, new()
        where T2 : T, new() => AddComponents(typeof(T1), typeof(T2));
    public IComponent<T> AddComponent(Type type);
    public IComponent<T1> AddComponent<T1>() where T1 : T, new();
    public IComponent<T>[] AddComponents(params Type[] types);
    public IComponent<T1> GetComponent<T1>() where T1 : T;
    public bool TryGetComponent<T1>(out IComponent<T1> component) where T1 : T;

    public IComponent<T> GetComponent(Type type);
    public bool ContainsComponent<T1>() where T1 : T;
    public bool ContainsComponent(Type type);
}