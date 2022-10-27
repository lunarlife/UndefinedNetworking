using UndefinedNetworking.GameEngine.UI.Elements;
using UndefinedNetworking.GameEngine.UI.Elements.Interfaces;
using Utils.Events;

namespace UndefinedNetworking.Events.UI.Window
{
    public abstract class UIEvent : Event
    {
        public abstract IUIElement Element { get; }
    }
}