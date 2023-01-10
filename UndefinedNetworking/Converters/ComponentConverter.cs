using System;
using Networking.DataConvert;
using Networking.Packets;
using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.Components;
using UndefinedNetworking.GameEngine.Scenes.UI.Components;

namespace UndefinedNetworking.Converters;

public class ComponentConverter : IDynamicDataConverter
{
    public bool IsValidConvertor(Type type) => typeof(IComponent).IsAssignableFrom(type);

    public byte[] Serialize(object o)
    {
        var data = ((IComponent)o).GetCloneData();
        var serialize =
            DataConverter.Serialize(data, switcher: (ushort)(int)ServerManagerBase.Type.GetRemoteType());
        var componentId = Component.GetComponentId(data.GetType());
        var view = (data as UIComponentData)?.TargetView;
        return DataConverter.Combine(DataConverter.Serialize(view?.Identifier), DataConverter.Serialize(componentId), serialize);
    }

    public object? Deserialize(byte[] data, Type type)
    {
        ushort index = 0;
        var identifier = DataConverter.Deserialize<uint>(data, ref index)!;
        var componentId = Component.GetComponentType(DataConverter.Deserialize<ushort>(data, ref index)!);
        var view = ServerManagerBase.ServerManager.GetView(identifier);
        var component = view.GetComponent(componentId) ?? view.AddComponent(componentId);
        component.ModifyLocal(componentData =>
        {
            DataConverter.DeserializeInject(data, componentData, ref index, switcher: (ushort)(int)ServerManagerBase.Type);
        });
        return component;
    }
}