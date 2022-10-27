using UndefinedNetworking.Events.UI.Window;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.UI.Windows.Interfaces
{
    public interface IFloating: IEventCaller<WindowFloatingEvent>
    {
        public bool IsCanFloatingNow { get; }
    }
}