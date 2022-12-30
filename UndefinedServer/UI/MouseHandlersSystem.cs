using UECS;
using UndefinedNetworking.Events.Mouse;
using UndefinedNetworking.GameEngine.UI.Components.Mouse;
using Utils.Events;

namespace UndefinedServer.UI;

public class MouseHandlersSystem : IAsyncSystem
{

    [ChangeHandler] private Filter<MouseUpHandlerComponent> _upHandlers;       
    [ChangeHandler] private Filter<MouseDownHandlerComponent> _downHandlers;
    [ChangeHandler] private Filter<MouseEnterHandlerComponent> _enterHandlers;
    [ChangeHandler] private Filter<MouseExitHandlerComponent> _exitHandlers;
    [AutoInject] private Filter<MouseHoldingHandlerComponent> _holdingHandlers;

    public void Init()
    {
        
    }

    public void Update()
    {
        foreach (var result in _holdingHandlers)
        {
            var component = result.Get1();
            if (!component.IsHolding) continue;
            component.CallEvent(new UIMouseHoldingEvent(component.TargetView));
        }
    }
}