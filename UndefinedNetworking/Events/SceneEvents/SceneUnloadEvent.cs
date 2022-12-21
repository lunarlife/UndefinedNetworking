using UndefinedNetworking.GameEngine.Scenes;

namespace UndefinedNetworking.Events.SceneEvents;

public class SceneUnloadEvent : SceneEvent
{
    public SceneUnloadEvent(IScene scene)
    {
        Scene = scene;
    }

    public override IScene Scene { get; }
}