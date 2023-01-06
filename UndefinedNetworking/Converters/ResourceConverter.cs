using System;
using Networking.DataConvert;
using UndefinedNetworking.GameEngine;
using UndefinedNetworking.GameEngine.Resources;

namespace UndefinedNetworking.Converters;

public class ResourceConverter : IDynamicDataConverter
{
    public bool IsValidConvertor(Type type) => typeof(IResource).IsAssignableFrom(type);

    public byte[] Serialize(object o) => DataConverter.Serialize(((IResource)o).Id);

    public object Deserialize(byte[] data, Type type) => ServerManagerBase.ServerManager.ResourcesManager.GetResource(DataConverter.Deserialize<int>(data)!);
}