using System.Collections.Generic;
using UndefinedNetworking.GameEngine.UI.Elements;
using UndefinedNetworking.GameEngine.UI.Elements.Interfaces;
using UndefinedNetworking.GameEngine.UI.Windows.Interfaces;

namespace UndefinedNetworking.GameEngine.UI.Windows.ContextMenu;

public interface IContextMenu : IUIElement, IGridable
{
    public IReadOnlyList<IContextMenuItem> Items { get; }
}