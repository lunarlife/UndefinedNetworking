namespace UndefinedNetworking.GameEngine.Scenes;

public interface ISceneViewer
{
    public IScene ActiveScene { get; }
    public void LoadScene(SceneType type);
}