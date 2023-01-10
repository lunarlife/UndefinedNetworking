using System.Collections.Generic;
using UndefinedNetworking;
using UndefinedNetworking.Exceptions;
using UndefinedNetworking.Gameplay.Chat;
using UndefinedNetworking.Packets.Player;
using UndefinedServer.Events;
using UndefinedServer.Events.Chats;
using UndefinedServer.Exceptions;
using Utils.Enums;
using Utils.Events;

namespace UndefinedServer.Chats
{
    public class ChatManager
    {
        private static readonly Enum<ChatType> EChats = new();
        private static bool _debugChatIsEnabled;
        private readonly ChatManager _instance;

        public static bool DebugChatIsEnabled
        {
            get => _debugChatIsEnabled;
            set
            {
                if(!Undefined.IsEnabled) throw new ChatException("you can change this only before enabled");
                _debugChatIsEnabled = value;
                if(_debugChatIsEnabled)
                    EChats.AddMember<DebugChat>("debug");
                else 
                    EChats.RemoveMember("debug");
            }
        }

        public ChatManager()
        {
            _instance = _instance is null ? this : throw new InstanceException($"{nameof(ChatManager)} is already exists");
            /*this.RegisterListener();*/
        }

        public static IReadOnlyList<ChatType> Chats => EChats.Values;
        
        public static T RegisterChat<T>() where T : ChatType, new()
        {
            var chat = new T();
            if (string.IsNullOrEmpty(chat.DisplayName))
                throw new CommandException(
                    $"Register chat {nameof(T)} failed. {nameof(chat.DisplayName)} cant be null or empty");
            EChats.AddMember(chat.DisplayName, chat);
            return chat;
        }
        
        
        /*[EventHandler]
        private void OnPacketReceive(PacketReceiveEventData e)
        {
            if (e.Packet is not ChatPacket packet) return;
            var player = Undefined.CurrentGame.GetPlayer(e.Client.Identifier);
            if (packet.Message is null) return;
            if (!EChats.Contains(packet.ChatTypeID))
            {
                player.Client.Disconnect(DisconnectCause.InvalidPacket, "unknown chat id");
                return;
            }
            var chat = EChats[packet.ChatTypeID];
            if (!chat.CanWriteMessages)
            {
                player.Client.Disconnect(DisconnectCause.InvalidPacket, $"cant write messages in chat {chat.DisplayName}");
                return;
            }
            if (!EChats.Contains(chat)) throw new ChatException("unknown chat");
            var ev = new ChatEvent(player, packet.Message, chat);
            /*EventManager.CallEvent(ev);#1#
            if (ev.IsCancelled || string.IsNullOrEmpty(ev.Message)) return;
            chat.Execute(player, ev.Message);
        }*/

        public static ChatType GetChat(string name)
        {
            if (!EChats.Contains(name)) throw new CommandException($"unknown command {name}");
            return EChats[name];
        }

        public static bool TryGetChat(string name, out ChatType? command)
        {
            if (EChats.Contains(name))
            {
                command = EChats[name];
                return true;
            }
            command = null;
            return false;
        }
        public static ChatType GetChat(int id)
        {
            if (!EChats.Contains(id)) throw new CommandException($"unknown command {id}");
            return EChats[id];
        }

        public static bool TryGetChat(int id, out ChatType? command)
        {
            if (EChats.Contains(id))
            {
                command = EChats[id];
                return true;
            }
            command = null;
            return false;
        }

        [EventHandler]
        private void OnServerClosed(ServerClosedEventData e)
        {
            /*EventManager.UnregisterEvents(this);*/
        }
    }
}