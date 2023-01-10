using UndefinedNetworking.Commands;
using UndefinedNetworking.Gameplay.Chat;
using Utils.Events;

namespace UndefinedServer.Events.Chats
{
    public class PlayerCommandEvent : EventData
    {
        public ICommand Command { get; }
        public UndefinedServer.Player Sender { get; }
        public ChatType ChatType { get; }

        public PlayerCommandEvent(ICommand command, UndefinedServer.Player sender, ChatType chatType)
        {
            Command = command;
            Sender = sender;
            ChatType = chatType;
        }
        
    }
}