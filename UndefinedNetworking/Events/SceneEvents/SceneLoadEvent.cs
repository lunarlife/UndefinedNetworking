using UndefinedNetworking.GameEngine.Scenes;

namespace UndefinedNetworking.Events.SceneEvents;

public class SceneLoadEvent : SceneEvent
{
    public SceneLoadEvent(IScene scene)
    {
        Scene = scene;
    }

    public override IScene Scene { get; }
}