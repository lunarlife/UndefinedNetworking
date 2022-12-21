using UndefinedNetworking.GameEngine.Objects;
using UndefinedNetworking.GameEngine.Scenes;

namespace UndefinedServer.GameEngine.Scenes;

public sealed class Scene3D : Scene<IGameObject3D>
{
    public Scene3D(ISceneViewer viewer) : base(viewer)
    {
    }
    
    public override SceneType Type => SceneType.XY;
}