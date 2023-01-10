using UndefinedNetworking.GameEngine.Scenes.UI;
using Utils.Events;

namespace UndefinedNetworking.Events.UIEvents;

public abstract class UIEventData : EventData
{
    public abstract IUIView View { get; }
}