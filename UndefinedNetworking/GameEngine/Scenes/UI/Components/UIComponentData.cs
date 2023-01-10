using Networking.DataConvert;
using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Scenes.UI.Views;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components;

public abstract record UIComponentData : ComponentData
{
    [ExcludeData] public new IUIViewBase TargetObject => (IUIViewBase)base.TargetObject;
}