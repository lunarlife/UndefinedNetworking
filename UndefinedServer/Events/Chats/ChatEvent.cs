using UndefinedNetworking.Gameplay.Chat;
using Utils.Events;

namespace UndefinedServer.Events.Chats
{
    public class ChatEvent : Event, ICancellable
    {
        public bool IsCancelled { get; set; }
        public UndefinedServer.Player Sender { get; }
        public string Message { get; set; }
        public ChatType ChatType { get; set; }
        
        public ChatEvent(UndefinedServer.Player sender, string message, ChatType chatType)
        {
            Sender = sender;
            Message = message;
            ChatType = chatType;
        }
    }
}