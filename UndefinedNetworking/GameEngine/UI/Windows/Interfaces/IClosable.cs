using UndefinedNetworking.Events.UI.Window;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.UI.Windows.Interfaces
{
    public interface IClosable : IEventCaller<WindowCloseEvent>
    {
        public bool CanCloseNow { get; }
    }
}