using System;
using System.Net;
using Networking;
using Networking.Packets;
using UndefinedNetworking;
using UndefinedNetworking.Packets.Player;
using Utils;
using Utils.Events;

namespace UndefinedServer
{
    internal sealed class Client
    {
        private Server _server;
        private RuntimePacketer? _packeter;
        internal Identifier Identifier { get; }
        internal bool WaitBeforeCloseServer => _packeter.HasPacketsToSend;
        internal delegate void DisconnectHandler(DisconnectCause cause, string message);
        internal event DisconnectHandler OnDisconnect;
        public IPAddress Address { get; }
        public int Port { get; }
        public Event<PacketReceiveEventData> OnPacketReceive => _packeter.OnReceive;
        internal Client(Server server, Identifier identifier)
        {
            Identifier = identifier;
            _server = server;
            Address = _server.Address!;
            Port = (int)_server.Port!;
            _packeter = new RuntimePacketer(server, Priority.Normal)
            {
                IsReading = true,
                IsSending = true
            };
            _packeter.UnhandledException += exception =>
            {
                 Undefined.Logger.Error(exception.Message + "\n" + exception.StackTrace);
                 Disconnect(DisconnectCause.Error, exception.StackTrace);
            };
        }
        internal void SendPacket(params Packet[] packets) => _packeter?.SendPacket(packets);
        internal void SendPacketNow(params Packet[] packets) => _packeter.SendPacketNow(packets);

        internal void Disconnect(DisconnectCause cause, string message)
        {
            try
            {
                _packeter?.SendPacketNow(new PlayerDisconnectPacket(cause, message));
            }
            catch(Exception e)
            {
                // ignored
            }
            finally
            {
                if (_packeter is not null)
                {
                    _packeter.IsSending = false;
                    _packeter.IsReading = false;
                }
                _server?.Close();
                _server = null;
                _packeter = null;
                OnDisconnect?.Invoke(cause, message);
            }
        }

        public void Request<T>(RequestPacket<T> packet, Action<T> callback) where T : Packet
        {
            _packeter.Request(packet, callback);
        }
    }
}