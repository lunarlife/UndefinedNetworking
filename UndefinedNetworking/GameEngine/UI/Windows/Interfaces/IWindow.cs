using System.Collections.Generic;
using UndefinedNetworking.GameEngine.UI.Elements;
using UndefinedNetworking.GameEngine.UI.Elements.Interfaces;

namespace UndefinedNetworking.GameEngine.UI.Windows.Interfaces;

public interface IWindow : IUIElement
{
    public const int MenuYSize = 30;
    public IReadOnlyList<IUIElement> Elements { get; }
    public void AddElement(IUIElement element);
    public void Remove(IUIElement element);
}