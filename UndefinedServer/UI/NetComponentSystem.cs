using UECS;
using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Scenes.UI.Components;
using UndefinedNetworking.Packets.Components;

namespace UndefinedServer.UI;

public class NetComponentSystem : IAsyncSystem
{
    [ChangeHandler] private Filter<IComponent<UINetworkComponentData>> _changedUIs;

    public void Init()
    {
        
    }

    public void Update()
    {
        foreach (var result in _changedUIs)
        {
            var res = result.Get1();
            res.Read(component =>
            {
                if(component.TargetView.Viewer is not Player player) return;
                player.Client.SendPacket(new UIComponentUpdatePacket(res));
            });
        }
    }
}
