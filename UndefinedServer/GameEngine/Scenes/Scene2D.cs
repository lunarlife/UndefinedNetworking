using UndefinedNetworking.GameEngine.Objects;
using UndefinedNetworking.GameEngine.Scenes;

namespace UndefinedServer.GameEngine.Scenes;

public sealed class Scene2D : Scene<IGameObject2D>
{
    public Scene2D(ISceneViewer viewer) : base(viewer)
    {
    }

    public override SceneType Type => SceneType.XY;
}