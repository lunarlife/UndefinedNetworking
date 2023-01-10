using UECS;
using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Scenes.UI.Components;
using UndefinedServer.Events;
using Utils.Events;

namespace UndefinedServer.UI;

public class NetComponentSystem : IAsyncSystem
{
    [ChangeHandler] private Filter<IComponent<UINetworkComponentData>> _changedUIs;
    public Event<ComponentRemoteUpdateEventData> OnRemoteUpdate { get; } = new();
    public void Init()
    {
        
    }

    public void Update()
    {
        foreach (var result in _changedUIs)
        {
            var res = result.Get1();
            if (!res.ShouldBeUpdatedRemote) return;
            res.ShouldBeUpdatedRemote = false;
            OnRemoteUpdate.Invoke(new ComponentRemoteUpdateEventData(res));
        }
    }
}
