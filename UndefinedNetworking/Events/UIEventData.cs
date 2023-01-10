using UndefinedNetworking.GameEngine.Scenes.UI;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;
using Utils.Events;

namespace UndefinedNetworking.Events
{
    public abstract class UIEventData : EventData
    {
        public abstract IUIViewBase View { get; }
    }
}