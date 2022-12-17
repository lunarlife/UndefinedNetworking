using System.Collections.Generic;
using System.Linq;
using UndefinedNetworking.Exceptions;

namespace UndefinedNetworking.GameEngine.UI;

public interface IUIViewer
{
    public IEnumerable<IUIView> ViewElements { get; }
    public IUIView Open(IUIElement element);
    public void Close(IUIView view);
    public bool IsOpened(IUIView view) => ViewElements.Contains(view);
}