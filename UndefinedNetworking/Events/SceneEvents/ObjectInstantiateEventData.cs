using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.Scenes;

namespace UndefinedNetworking.Events.SceneEvents;

public class ObjectInstantiateEventData : SceneEventData
{
    public ObjectInstantiateEventData(IScene scene, IObjectBase gameObject)
    {
        Scene = scene;
        GameObject = gameObject;
    }

    public override IScene Scene { get; }
    public IObjectBase GameObject { get; }
}