using System;
using Networking.DataConvert;

namespace UndefinedNetworking.GameEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ClientDataAttribute : DataSwitchAttribute
{
    public const int DataId = 1;
    public ClientDataAttribute() : base(DataId)
    {
    }
}