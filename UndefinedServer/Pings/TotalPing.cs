using System;
using UndefinedNetworking.Packets.Server;
using UndefinedServer.Events;
using UndefinedServer.Events.PlayerEvents.PingEvents;
using Utils.Events;

namespace UndefinedServer.Pings;

public class TotalPing : Ping, IEventCaller<TotalPingUpdateEvent>
{
    private readonly Player _player;
    private int _delay;
    private DateTime _lastPingUpdate = DateTime.Now;
    private int _invalidRequestsCount;

    public override int Delay => _delay;

    public override DateTime LastPingUpdate => _lastPingUpdate;

    public override int InvalidRequestsCount => _invalidRequestsCount;
    public override void Update()
    {
        _lastPingUpdate = DateTime.Now;
        _player.Client.SendPacket(new ClientPingPacket());
        _invalidRequestsCount++;
    }

    internal TotalPing(Player player)
    {
        _player = player;
        EventManager.RegisterEvents(this);
    }
    [EventHandler]
    private void OnPacketReceive(PacketReceiveEvent e)
    {
        var time = DateTime.Now;
        _delay = (int)(time - _lastPingUpdate).TotalMilliseconds;
        _lastPingUpdate = time;
        if (_invalidRequestsCount > 0) 
            _invalidRequestsCount--;
        this.CallEvent(new TotalPingUpdateEvent(_player));
    }
}