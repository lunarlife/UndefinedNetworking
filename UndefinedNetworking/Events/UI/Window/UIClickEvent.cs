using UndefinedNetworking.GameEngine.UI.Elements;
using UndefinedNetworking.GameEngine.UI.Elements.Interfaces;

namespace UndefinedNetworking.Events.UI.Window
{
    public class UIClickEvent : UIEvent
    {
        public override IUIElement Element { get; }
    
        public UIClickEvent(IUIElement element)
        {
            Element = element;
        }
    }
}