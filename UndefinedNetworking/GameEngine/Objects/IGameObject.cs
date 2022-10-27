namespace UndefinedNetworking.GameEngine.Objects;

public interface IGameObject : IObjectCore
{
    public ITransform Transform { get; }

}