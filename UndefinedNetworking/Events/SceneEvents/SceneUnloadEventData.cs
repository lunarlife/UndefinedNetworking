using UndefinedNetworking.GameEngine.Scenes;

namespace UndefinedNetworking.Events.SceneEvents;

public class SceneUnloadEventData : SceneEventData
{
    public SceneUnloadEventData(IScene scene)
    {
        Scene = scene;
    }

    public override IScene Scene { get; }
}