using UndefinedNetworking.Gameplay.Chat;
using Utils;

namespace UndefinedNetworking.Chats
{
    public class ChatMessage
    {
        public string Title { get; }
        public string Text { get; }
        public Color Color { get; }
        public ChatType Chat { get; }

        public ChatMessage(ISender sender, string text, Color color, ChatType chat)
        {
            Title = sender.SenderName;
            Text = text;
            Color = color;
            Chat = chat;
        }
        public ChatMessage(ISender sender, string text, ChatType chat)
        {
            Title = sender.SenderName;
            Text = text;
            Color = Color.White;
            Chat = chat;
        }
        public ChatMessage(string title, string text, ChatType chat)
        {
            Title = title;
            Text = text;
            Color = Color.White;
            Chat = chat;
        }
        public static implicit operator string(ChatMessage message) => message.Text;
    }
}