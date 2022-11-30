using System;
using System.Linq;
using Networking;
using Networking.DataConvert;
using UECS;
using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.UI.Components;
using UndefinedNetworking.Packets.UI;

namespace UndefinedServer.UI;

public class NetComponentSystem : ISystem, IDynamicDataConverter
{
    [AutoInject] private Filter<UINetworkComponent> _ui = null;

    public void Init()
    {
        DataConverter.AddDynamicConverter(this);
    }

    public void Update()
    {
        foreach (var result in _ui)
        {
            var ui = result.Get1();
            var component = ui as INetworkComponent;
            if (!component.IsChanged || component.IsNetInitialized) continue;
            if(ui.TargetView.Viewer is not Player player) continue;
            player.Client.SendPacket(new ComponentUpdatePacket(component));
        }
    }

    public Type Type => typeof(INetworkComponent);
    public byte[] Serialize(object o)
    {
        var nc = (INetworkComponent)o;
        var serialize = nc.IsNetInitialized
            ? DataConverter.Serialize(o, ServerDataAttribute.DataId)
            : DataConverter.Serialize(o);
        var identifier = DataConverter.Serialize(nc.NetIdentifier);
        var buffer = new byte[serialize.Length + identifier.Length];
        identifier.CopyTo(buffer, 0);
        serialize.CopyTo(buffer, identifier.Length);
        return buffer;
    }

    public object? Deserialize(byte[] data, Type type)
    {
        var identifier = DataConverter.Deserialize<Identifier>(data);
        return _ui.FirstOrDefault(nc => nc.Get1().NetIdentifier == identifier);
    }
}
