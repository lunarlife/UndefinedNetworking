using System;
using System.Collections.Generic;
using System.Linq;
using Networking;
using UndefinedNetworking.Exceptions;
using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.UI;
using UndefinedNetworking.GameEngine.UI.Components;
using UndefinedServer.Exeptions;
using UndefinedServer.GameEngine;

namespace UndefinedServer.UI.View;

public class UIView : ObjectCore, IUIView
{
    private readonly List<UIComponent> _components = new();
    private readonly List<UIView> _childs;
    public IUIViewer Viewer { get; }
    public IRectTransform Transform { get; }
    public IUIElement TargetElement { get; }
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
        foreach (var c in element.Components)
        {
            AddComponent(c.GetType());
        }
        _childs = element.Childs.Select(ch => new UIView(ch, viewer, this)).ToList();
    }

    public UIComponent AddComponent(Type type)
    {
        if (!type.IsSubclassOf(typeof(UIComponent))) throw new ComponentException($"type is not {nameof(UIComponent)}");
        if (type.GetConstructors().All(c => c.GetParameters().Length != 0))
            throw new ComponentException("type is not contains empty constructor");
        var component = (Activator.CreateInstance(type) as UIComponent)!;
        component.Init();
        return component;
    }

    public T? GetComponentOfType<T>() where T : UIComponent => _components.FirstOrDefault(c => c.GetType() == typeof(T)) as T;
    public UIComponent? GetComponentOfType(Type type) => type.IsSubclassOf(typeof(UIComponent)) ? _components.FirstOrDefault(c => c.GetType() == type) : throw new ViewException("Type is not component");
    
    
    
    
    public void Close()
    {
        Viewer.Close(this);
    }
    protected override void DoDestroy()
    {
        
    }

    protected virtual void OnDestroy()
    {
        
    }

}