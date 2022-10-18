using UndefinedNetworking.Commands;
using UndefinedNetworking.Gameplay.Chat;
using Utils.Events;

namespace UndefinedServer.Events.Chats
{
    public class PlayerCommandEvent : Event
    {
        public ICommand Command { get; }
        public ServerPlayer Sender { get; }
        public ChatType ChatType { get; }

        public PlayerCommandEvent(ICommand command, ServerPlayer sender, ChatType chatType)
        {
            Command = command;
            Sender = sender;
            ChatType = chatType;
        }
        
    }
}