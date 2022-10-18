using UndefinedNetworking.Window.UI;

namespace UndefinedNetworking.Events.Window.UI;

public abstract class UIClickEvent<T> : UIEvent<T> where T : IUIElement
{
    public override T Element { get; }
    
    public UIClickEvent(T element)
    {
        Element = element;
    }
}