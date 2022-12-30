using System;
using System.Linq;
using Networking;
using Networking.DataConvert;
using UECS;
using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.UI.Components;
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
        return DataConverter.Combine(DataConverter.Serialize(componentId), DataConverter.Serialize(view?.Identifier), serialize);
    }

    public object? Deserialize(byte[] data, Type type)
    {
        ushort index = 0;
        var identifier = DataConverter.Deserialize<Identifier>(data, ref index)!;
        var result = _uis.FirstOrDefault(nc => nc.Get1().Identifier == identifier).Get1();
        if (result is null)
            return null;
        DataConverter.DeserializeInject(data, result, ref index, switcher: ServerDataAttribute.DataId, converterUsing: ConvertType.ExcludeCurrent);
        return result;
    }
}
