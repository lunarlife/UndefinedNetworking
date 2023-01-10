using UECS;
using UndefinedNetworking.Events.Mouse;
using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Scenes.UI.Components.Mouse;
using Utils.Events;

namespace UndefinedServer.UI;

public class MouseHandlersSystem : IAsyncSystem
{

    [ChangeHandler] private Filter<Component<MouseUpHandler>> _upHandlers;       
    [ChangeHandler] private Filter<Component<MouseDownHandler>> _downHandlers;
    [ChangeHandler] private Filter<Component<MouseEnterHandler>> _enterHandlers;
    [ChangeHandler] private Filter<Component<MouseExitHandler>> _exitHandlers;
    [AutoInject] private Filter<Component<MouseHoldingHandler>> _holdingHandlers;

    public void Init()
    {
        
    }

    public void Update()
    {
        foreach (var result in _holdingHandlers)
        {
            result.Get1().Read(component =>
            {
                if(!component.IsHolding) return;
                component.Event.Invoke(new MouseHoldingEventData(component.TargetView));
            });
        }
    }
}