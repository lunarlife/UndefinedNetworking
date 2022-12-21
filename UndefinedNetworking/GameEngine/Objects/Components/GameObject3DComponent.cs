using Networking.DataConvert;

namespace UndefinedNetworking.GameEngine.Objects.Components;

public record GameObject3DComponent : Component
{
    [ExcludeData] private IGameObject3D GameObject { get; }
}