using UndefinedNetworking.GameEngine.Scenes;
using Utils.Events;

namespace UndefinedNetworking.Events.SceneEvents;

public abstract class SceneEvent : Event
{
    public abstract IScene Scene { get; }
}