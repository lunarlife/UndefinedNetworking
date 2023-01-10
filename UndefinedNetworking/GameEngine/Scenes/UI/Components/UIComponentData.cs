using Networking.DataConvert;
using UndefinedNetworking.GameEngine.Components;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components;

public abstract record UIComponentData : ComponentData
{

    [ExcludeData] public IUIView TargetView { get; private set; }
}