using Networking.DataConvert;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components;

public abstract record UIComponent : Component
{
    [ExcludeData] public IUIView TargetView { get; private set; }
}