using UndefinedNetworking.GameEngine.Scenes;
using Utils.Events;

namespace UndefinedNetworking.Events.SceneEvents;

public abstract class SceneEventData : EventData
{
    public abstract IScene Scene { get; }
}