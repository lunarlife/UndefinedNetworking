using System.Collections.Generic;
using System.Linq;
using UndefinedServer.Exeptions;

namespace UndefinedNetworking.GameEngine.UI;

public interface IUIViewer
{
    public IEnumerable<IUIView> ViewElements { get; }
    public IUIView Open(IUIElement element);
    public void Close(IUIView view);

    public void UpdateView(IUIView view)
    {
        if (!ViewElements.Contains(view)) throw new ViewException("");
        OnUpdateView(view);
    }
    protected void OnUpdateView(IUIView view);
}