using System;
using System.Net.NetworkInformation;
using UndefinedServer.Events.PlayerEvents.PingEvents;
using Utils.Events;

namespace UndefinedServer.Pings;

public class NetworkPing : Ping
{
    private readonly Player _player;
    private readonly System.Net.NetworkInformation.Ping _pinger;
    private int _delay;
    private DateTime _lastPingUpdate = DateTime.Now;
    private int _invalidRequestsCount;

    public override int Delay => _delay;

    public override DateTime LastPingUpdate => _lastPingUpdate;

    public override int InvalidRequestsCount => _invalidRequestsCount;
    public Event<NetworkPingUpdateEventData> UpdatePing { get; } = new();

    public override void Update()
    {
        if(!_player.IsOnline) Dispose();
        _pinger.SendAsync(_player.Client.Address, Undefined.ServerConfiguration.MaxPlayerPing,null);
        _invalidRequestsCount++;
    }

    internal NetworkPing(Player player)
    {
        _pinger = new System.Net.NetworkInformation.Ping();
        _pinger.PingCompleted += Update;
        _player = player;
        EventManager.RegisterEvents(this);
    }
    
    private void Update(object sender, PingCompletedEventArgs pingCompletedEventArgs)
    {
        if(pingCompletedEventArgs.Reply is not { } reply) return;
        _lastPingUpdate = DateTime.Now;
        _delay = (int)reply.RoundtripTime;
        if (_invalidRequestsCount > 0) 
            _invalidRequestsCount--;
        UpdatePing.Invoke(new NetworkPingUpdateEventData(_player));
    }
    public override void Dispose()
    {
        EventManager.UnregisterEvents(this);
    }
}