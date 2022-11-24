using Networking;
using Networking.Packets;
using UndefinedNetworking;
using UndefinedNetworking.Packets.Player;
using UndefinedServer.Events;
using Utils;
using Utils.Events;

namespace UndefinedServer
{
    internal sealed class Client
    {
        private Server _server;
        private RuntimePacketer _packeter;
        internal Identifier Identifier { get; }
        internal bool WaitBeforeCloseServer => _packeter.HasPacketsToSend;
        internal Client(Server server, Identifier identifier)
        {
            Identifier = identifier;
            _server = server;
            _packeter = new RuntimePacketer(server, Priority.Normal)
            {
                IsReading = true,
                IsSending = true
            };
            _packeter.Receive += OnReceive;
            _packeter.UnhandledException += exception => Undefined.Logger.Error(exception.Message + "\n" + exception.StackTrace);
        }
        internal void SendPacket(params Packet[] packets)
        {
            _packeter.SendPacket(packets);
        }

        internal void Disconnect(DisconnectCause cause, string message)
        {
            _packeter.SendPacketNow(new PlayerDisconnectPacket(Identifier, cause, message));
            _packeter.IsSending = false;
            _packeter.IsReading = false;
            _server.Close();
            _server = null;
            _packeter = null;
        }
        
        private void OnReceive(Packet packet)
        {
            EventManager.CallEvent(new PacketReceiveEvent(packet, this));
        }
    }
}