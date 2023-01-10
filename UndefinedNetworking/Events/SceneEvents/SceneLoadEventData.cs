using UndefinedNetworking.GameEngine.Scenes;

namespace UndefinedNetworking.Events.SceneEvents;

public class SceneLoadEventData : SceneEventData
{
    public SceneLoadEventData(IScene scene)
    {
        Scene = scene;
    }

    public override IScene Scene { get; }
}