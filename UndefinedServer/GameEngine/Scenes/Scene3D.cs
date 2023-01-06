using UndefinedNetworking.GameEngine.Scenes;
using UndefinedNetworking.GameEngine.Scenes.Objects;

namespace UndefinedServer.GameEngine.Scenes;

public sealed class Scene3D : Scene<IGameObject3D>
{
    public Scene3D(ISceneViewer viewer) : base(viewer)
    {
    }
    
    public override SceneType Type => SceneType.XY;
}