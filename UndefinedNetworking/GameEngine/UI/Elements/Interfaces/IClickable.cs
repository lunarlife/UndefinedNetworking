using UndefinedNetworking.Events.UI.Window;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.UI.Elements.Interfaces
{
    public interface IClickable : IEventCaller<UIClickEvent> 
    {
        public bool IsCanClickNow { get; }
        public MouseKey HandleClicks { get; }
        public ClickState HandleClickStates { get; }
    }
}