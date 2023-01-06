using Networking.DataConvert;

namespace UndefinedNetworking.GameEngine.Scenes.Objects.Components;

public record GameObject3DComponent : Component
{
    [ExcludeData] private IGameObject3D GameObject { get; }
}