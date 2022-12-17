using System;
using System.Collections.Generic;
using Networking;
using UndefinedNetworking.GameEngine.UI.Components;

namespace UndefinedNetworking.GameEngine.UI;

public interface IUIView
{
    public IRectTransform Transform { get; }
    public Identifier Identifier { get; }
    public IUIViewer Viewer { get; }
    public IUIElement TargetElement { get; }
    public IEnumerable<Component> Components { get; }

    public T AddComponent<T>() where T : UIComponent, new();
    public UIComponent[] AddComponents<T, T1, T2, T3, T4>()
        where T : UIComponent, new()
        where T1 : UIComponent, new()
        where T2 : UIComponent, new()
        where T3 : UIComponent, new()
        where T4 : UIComponent, new() => AddComponents(typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
    public UIComponent[] AddComponents<T, T1, T2, T3>()
        where T : UIComponent, new()
        where T1 : UIComponent, new()
        where T2 : UIComponent, new()
        where T3 : UIComponent, new() => AddComponents(typeof(T), typeof(T1), typeof(T2), typeof(T3));
    public UIComponent[] AddComponents<T, T1, T2>()
        where T : UIComponent, new()
        where T1 : UIComponent, new()
        where T2 : UIComponent, new() => AddComponents(typeof(T), typeof(T1), typeof(T2));
    public UIComponent[] AddComponents<T, T1>()
        where T : UIComponent, new()
        where T1 : UIComponent, new() => AddComponents(typeof(T), typeof(T1));
    public UIComponent AddComponent(Type type);
    public UIComponent[] AddComponents(params Type[] types);
    public T? GetComponentOfType<T>() where T : UIComponent;
    public void Destroy();
    public UIComponent? GetComponentOfType(Type type);
    public void Close();
    public bool ContainsComponent<T>() where T : UIComponent;
    public bool ContainsComponent(Type type);
}