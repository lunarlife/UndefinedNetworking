using Networking.DataConvert.Handlers;
using UndefinedNetworking.Events.InputFieldEvents;
using UndefinedNetworking.GameEngine.Scenes.UI.Enums;
using UndefinedNetworking.GameEngine.Scenes.UI.Structs;
using Utils;
using Utils.Dots;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.Scenes.UI.Components;

public sealed record InputField : UINetworkComponentData, IClientChangeHandler, IDeserializeHandler
{
    public Event<InputFieldTextChangedData> Event { get; } = new();

    public void OnDeserialize()
    {
        Event.Invoke(new InputFieldTextChangedData(this));
    }
    
    public string Text { get; set; } = "";
    
    [ClientData]
    public FontStyle FontStyle { get; set; }

    [ClientData]
    public Color Color { get; set; }

    [ClientData]
    public TextWrapping Wrapping { get; set; }

    [ClientData]
    public FontSize Size { get; set; }

    [ClientData]
    public Dot2Int CaretSize { get; set; }
}