using System;
using Networking.DataConvert;

namespace UndefinedNetworking.GameEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ServerDataAttribute : DataSwitchAttribute
{
    public const int DataId = 0;
    public ServerDataAttribute() : base(DataId)
    {
    }
}