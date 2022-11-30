using System;
using System.Collections.Generic;
using Networking;
using UndefinedNetworking.GameEngine.UI.Components;

namespace UndefinedNetworking.GameEngine.UI;

public interface IUIView
{
    public IRectTransform Transform { get; }
    public IEnumerable<UIComponent> Components { get; }
    public Identifier Identifier { get; }
    public IUIViewer Viewer { get; }
    public IUIElement TargetElement { get; }

    public T? AddComponent<T>() where T : UIComponent, new() => AddComponent(typeof(T)) as T;
    public UIComponent? AddComponent(Type type);
    public T? GetComponentOfType<T>() where T : UIComponent => GetComponentOfType(typeof(T)) as T;
    public void Destroy();
    public UIComponent? GetComponentOfType(Type type);
}