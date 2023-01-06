using UndefinedNetworking.GameEngine.Scenes.UI;
using Utils.Events;

namespace UndefinedNetworking.Events
{
    public abstract class UIEvent : Event
    {
        public abstract IUIView View { get; }
    }
}