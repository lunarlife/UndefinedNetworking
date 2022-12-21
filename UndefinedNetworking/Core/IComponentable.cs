using System;
using System.Collections.Generic;
using UndefinedNetworking.Events.Components;
using UndefinedNetworking.GameEngine;
using Utils.Events;

namespace UndefinedNetworking.Core;

public interface IComponentable<T> : IEventCaller<ComponentAddEvent> where T : Component
{
    public IEnumerable<T> Components { get; }
    public T[] AddComponents<T1, T2, T3, T4, T5>()
        where T1 : T, new()
        where T2 : T, new()
        where T3 : T, new()
        where T4 : T, new()
        where T5 : T, new() => AddComponents(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
    public T[] AddComponents<T1, T2, T3, T4>()
        where T1 : T, new()
        where T2 : T, new()
        where T3 : T, new()
        where T4 : T, new() => AddComponents(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
    public T[] AddComponents<T1, T2, T3>()
        where T1 : T, new()
        where T2 : T, new()
        where T3 : T, new() => AddComponents(typeof(T1), typeof(T2), typeof(T3));
    public T[] AddComponents<T1, T2>()
        where T1 : T, new()
        where T2 : T, new() => AddComponents(typeof(T1), typeof(T2));
    public T AddComponent(Type type);
    public T1 AddComponent<T1>() where T1 : T, new();
    public T[] AddComponents(params Type[] types);
    public T1? GetComponentOfType<T1>() where T1 : T;

    public T? GetComponentOfType(Type type);
    public bool ContainsComponent<T1>() where T1 : T;
    public bool ContainsComponent(Type type);
}