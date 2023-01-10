using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UndefinedNetworking.Events.UIEvents;
using UndefinedNetworking.Exceptions;
using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Scenes;
using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Components;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;
using Utils.Events;

namespace UndefinedServer.UI.View;

public abstract class UIView : IUIViewBase
{
    private static readonly PropertyInfo TargetViewProperty = typeof(ComponentData).GetProperty("TargetObject", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)!;
    
    private static readonly List<UIView> Views = new();

    private readonly List<IComponent<UIComponentData>> _components = new();
    public IComponent<UIComponentData>[] Components => _components.ToArray();
    public Event<UICloseEventData> OnClose { get; } = new();
    public IComponent<RectTransform> Transform { get; }
    public uint Identifier { get; }

    internal UIView(ViewParameters parameters)
    {
        Identifier = (ushort)Views.Count;
        Views.Add(this);
        var rectTransform = (IComponent<RectTransform>)AddComponentLocal(typeof(RectTransform));
        rectTransform.Modify(data =>
        {
            data.ApplyParameters(parameters);
        });
        Transform = rectTransform;
    }

    public IComponent<T> AddComponent<T>() where T : UIComponentData, new() => (IComponent<T>)AddComponent(typeof(T));

    public IComponent<UIComponentData> AddComponent(Type type)
    {
        var component = AddComponentLocal(type);
        return component;
    }
    private IComponent<UIComponentData> AddComponentLocal(Type type)
    {
        if (!type.IsSubclassOf(typeof(UIComponentData))) throw new ComponentException($"type is not {nameof(UIComponentData)}");
        var component = Component<UIComponentData>.CreateInstance(type);
        component.Modify(data =>
        {
            TargetViewProperty.SetValue(data, this);

        });
        RequireComponent.AddRequirements(component, this);
        _components.Add(component);
        return component;
    }
    public IComponent<UIComponentData>[] AddComponents(params Type[] types) => types.Select(AddComponent).ToArray();

    public IComponent<T> GetComponent<T>() where T : UIComponentData => _components.FirstOrDefault(c => c.ComponentType == typeof(T)) as IComponent<T> ?? throw new ComponentException("component not found");
    public bool TryGetComponent<T1>(out IComponent<T1> component) where T1 : UIComponentData
    {
        component = _components.FirstOrDefault(c => c.ComponentType == typeof(T1)) as IComponent<T1>;
        return component is not null;
    }

    public abstract void Destroy();

    public IComponent<UIComponentData> GetComponent(Type type) => type.IsSubclassOf(typeof(UIComponentData)) ? _components.FirstOrDefault(c => c.ComponentType == type) : throw new ViewException("Type is not component");
    
    public void Close()
    {
        Destroy();
    }

    public abstract bool ContainsViewer(ISceneViewer viewer);

    public bool ContainsComponent<T>() where T : UIComponentData => ContainsComponent(typeof(T));

    public bool ContainsComponent(Type type) => _components.FirstOrDefault(c => c.GetType() == type) != null;

    public static UIView GetView(uint identifier) => Views.Count > identifier
        ? Views[(int)identifier] 
        : throw new ViewException("view not found");  
    public static bool TryGetView(uint identifier, out UIView view)
    {
        view = null;
        return (Views.Count > identifier
            ? view = Views[(int)identifier]
            : null) is not null;
    }
}