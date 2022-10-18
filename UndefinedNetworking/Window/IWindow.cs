using System.Collections.Generic;
using Networking.Window.UI;

namespace Networking.Window;

public interface IWindow
{
    public IReadOnlyList<IUIElement> Elements { get; }
    public void AddElement(IUIElement element);
    public void Remove(IUIElement element);
}