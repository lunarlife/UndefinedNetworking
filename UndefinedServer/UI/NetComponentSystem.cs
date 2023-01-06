using System;
using System.Linq;
using Networking;
using Networking.DataConvert;
using UECS;
using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.Scenes.UI.Components;
using UndefinedNetworking.Packets.Components;
using UndefinedServer.UI.View;

namespace UndefinedServer.UI;

public class NetComponentSystem : IAsyncSystem, IDynamicDataConverter
{
    [ChangeHandler(true)] private Filter<UINetworkComponent> _changedUIs;
    [AutoInject(true)] private Filter<UINetworkComponent> _uis;

    public void Init()
    {

    }

    public void Update()
    {
        foreach (var result in _changedUIs)
        {
            var ui = result.Get1();
            if(ui.TargetView.Viewer is not Player player) continue;
            player.Client.SendPacket(new UIComponentUpdatePacket(ui));
        }
    }

    public bool IsValidConvertor(Type type) => typeof(INetworkComponent).IsAssignableFrom(type);

    public byte[] Serialize(object o)
    {
        var serialize =
            DataConverter.Serialize(o, switcher: ClientDataAttribute.DataId, converterUsing: ConvertType.ExcludeCurrent);
        var componentId = Component.GetComponentId(o.GetType());
        var view = (o as UIComponent)?.TargetView;
        return DataConverter.Combine(DataConverter.Serialize(view?.Identifier), DataConverter.Serialize(componentId), serialize);
    }

    public object? Deserialize(byte[] data, Type type)
    {
        ushort index = 0;
        var identifier = DataConverter.Deserialize<uint>(data, ref index)!;
        var componentId = Component.GetComponentType(DataConverter.Deserialize<ushort>(data, ref index)!);
        var view = UIView.GetView(identifier);
        var component = view.GetComponent(componentId);
        DataConverter.DeserializeInject(data, component, ref index, switcher: ServerDataAttribute.DataId);
        return component;
    }
}
