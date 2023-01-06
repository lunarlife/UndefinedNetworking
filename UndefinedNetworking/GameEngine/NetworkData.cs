using Networking.DataConvert;
using Networking.Packets;
using UndefinedNetworking.Converters;
using UndefinedNetworking.Exceptions;

namespace UndefinedNetworking.GameEngine;

public static class NetworkData
{
    private static bool _isLoaded;
    
    public static void LoadNetworkData()
    {
        if (_isLoaded) throw new NetworkDataException("data is already loaded");
        DataConverter.AddStaticConverter(new ColorConverter());
        DataConverter.AddStaticConverter(new RectConverter());
        DataConverter.AddDynamicConverter(new ResourceConverter());
        Packet.LoadPackets();
        Component.LoadComponents();
        _isLoaded = true;
    }
}