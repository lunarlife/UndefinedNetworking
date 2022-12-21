using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Networking;
using UndefinedNetworking.Exceptions;
using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.Scenes;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.GameEngine.UI.Components;
using Utils;

namespace UndefinedServer.UI.View;

public sealed class UIView : IUIView
{
    private static readonly PropertyInfo TargetViewProperty = typeof(UIComponent).GetProperty("TargetView", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)!;
    private readonly List<UIComponent> _components = new();
    public ISceneViewer Viewer { get; }
    public IEnumerable<UIComponent> Components => _components;
    public RectTransformBase Transform { get; }
    public Identifier Identifier { get; }

    internal UIView(ISceneViewer viewer, ViewParameters parameters)
    {
        Identifier = new Identifier();
        Viewer = viewer;
        var rectTransform = AddComponent<RectTransform>();
        rectTransform.ApplyParameters(parameters);
        Transform = rectTransform;
    }

    public T AddComponent<T>() where T : UIComponent, new() => (AddComponent(typeof(T)) as T)!;

    public UIComponent AddComponent(Type type)
    {
        if (!type.IsSubclassOf(typeof(UIComponent))) throw new ComponentException($"type is not {nameof(UIComponent)}");
        var ctor = ReflectionUtils.GetConstructor(type);
        if (ctor == null) throw new ComponentException("component has no empty constructor");
        var component = (ctor.Invoke(null) as UIComponent)!;
        Undefined.CurrentGame.Systems.Add(component);
        TargetViewProperty.SetValue(component, this);
        _components.Add(component); 
        return component;
    }

    public UIComponent[] AddComponents(params Type[] types) => types.Select(AddComponent).ToArray();

    public T? GetComponentOfType<T>() where T : UIComponent => _components.FirstOrDefault(c => c.GetType() == typeof(T)) as T;
    public void Destroy()
    {
        
    }

    public UIComponent? GetComponentOfType(Type type) => type.IsSubclassOf(typeof(UIComponent)) ? _components.FirstOrDefault(c => c.GetType() == type) : throw new ViewException("Type is not component");
    
    public void Close()
    {
        Destroy();
    }

    public bool ContainsComponent<T>() where T : UIComponent => ContainsComponent(typeof(T));

    public bool ContainsComponent(Type type) => _components.FirstOrDefault(c => c.GetType() == type) != null;
}