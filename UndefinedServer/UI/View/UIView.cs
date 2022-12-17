using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Networking;
using UndefinedNetworking.Exceptions;
using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.GameEngine.UI.Components;
using UndefinedServer.Exeptions;
using UndefinedServer.GameEngine;

namespace UndefinedServer.UI.View;

public sealed class UIView : ObjectCore, IUIView
{
    private static readonly PropertyInfo TargetViewProperty = typeof(UIComponent).GetProperty("TargetView", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)!;
    private readonly List<UIComponent> _components = new();
    private readonly List<UIView> _childs;
    public IUIViewer Viewer { get; }
    public IRectTransform Transform { get; }
    public IUIElement TargetElement { get; }
    IEnumerable<Component> IUIView.Components => Components;

    public Identifier Identifier { get; }

    public IEnumerable<UIComponent> Components => _components;
    internal UIView(IUIElement element, IUIViewer viewer, UIView? parent)
    {
        Identifier = new Identifier();
        Viewer = viewer;
        TargetElement = element;
        var parameters = element.CreateNewView(viewer);
        Transform = new RectTransform(this, parent?.Transform, parameters.IsActive,
            parameters.Layer, parameters.Margins, parameters.OriginalRect, parameters.Pivot, parameters.Bind);
        _childs = element.Childs.Select(ch => new UIView(ch, viewer, this)).ToList();
        element.OnCreateView(this);
    }

    public T? AddComponent<T>() where T : UIComponent, new() => AddComponent(typeof(T)) as T;

    public UIComponent AddComponent(Type type)
    {
        if (!type.IsSubclassOf(typeof(UIComponent))) throw new ComponentException($"type is not {nameof(UIComponent)}");
        var component = (Activator.CreateInstance(type) as UIComponent)!;
        Undefined.CurrentGame.Systems.Add(component);
        TargetViewProperty.SetValue(component, this);
        _components.Add(component); 
        return component;
    }

    public UIComponent?[] AddComponents(params Type[] types) => types.Select(AddComponent).ToArray();

    public T? GetComponentOfType<T>() where T : UIComponent => _components.FirstOrDefault(c => c.GetType() == typeof(T)) as T;
    public UIComponent? GetComponentOfType(Type type) => type.IsSubclassOf(typeof(UIComponent)) ? _components.FirstOrDefault(c => c.GetType() == type) : throw new ViewException("Type is not component");
    
    public void Close()
    {
        Viewer.Close(this);
    }

    public bool ContainsComponent<T>() where T : UIComponent => ContainsComponent(typeof(T));

    public bool ContainsComponent(Type type) => _components.FirstOrDefault(c => c.GetType() == type) != null;

    protected override void DoDestroy()
    {
        
    }
}