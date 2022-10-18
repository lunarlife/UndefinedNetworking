using System.Collections.Generic;
using UndefinedNetworking.Window.UI;

namespace UndefinedNetworking.Window;

public interface IWindow : IUIElement
{
    public IReadOnlyList<IUIElement> Elements { get; }
    public void AddElement(IUIElement element);
    public void Remove(IUIElement element);
}