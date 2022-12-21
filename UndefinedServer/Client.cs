using System.Net;
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
        internal delegate void DisconnectHandler(DisconnectCause cause, string message);
        internal event DisconnectHandler OnDisconnect;
        public IPAddress Address { get; }
        public int Port { get; }
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
            _packeter.Receive += OnReceive;
            _packeter.UnhandledException += exception => Undefined.Logger.Error(exception.Message + "\n" + exception.StackTrace);
        }
        internal void SendPacket(params Packet[] packets) => _packeter.SendPacket(packets);
        internal void SendPacketNow(params Packet[] packets) => _packeter.SendPacketNow(packets);

        internal void Disconnect(DisconnectCause cause, string message)
        {
            var s = "{\n    \"version\": {\n        \"name\": \"1.19\",\n        \"protocol\": 759\n    },\n    \"players\": {\n        \"max\": 100,\n        \"online\": 5,\n        \"sample\": [\n            {\n                \"name\": \"thinkofdeath\",\n                \"id\": \"4566e69f-c907-48ee-8d71-d7ba5aa00d20\"\n            }\n        ]\n    },\n    \"description\": {\n        \"text\": \"Hello world\"\n    },\n    \"favicon\": \"data:image/png;base64,<data>\",\n    \"previewsChat\": true,\n    \"enforcesSecureChat\": true,\n}";
            try
            {
                _packeter.SendPacketNow(new PlayerDisconnectPacket(Identifier, cause, message));
            }
            catch
            {
                // ignored
            }
            finally
            {
                _packeter.IsSending = false;
                _packeter.IsReading = false;
                _server.Close();
                _server = null;
                _packeter = null;
                OnDisconnect?.Invoke(cause, message);
            }
        }
        
        private void OnReceive(Packet packet)
        {
            EventManager.CallEvent(new PacketReceiveEvent(packet, this));
        }
    }
}