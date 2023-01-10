using Networking.DataConvert;
using UndefinedNetworking.GameEngine.Components;

namespace UndefinedNetworking.GameEngine.Scenes.Objects.Components;

public record GameObject3DComponent : ComponentData
{
    [ExcludeData] private IGameObject3D GameObject { get; }
}