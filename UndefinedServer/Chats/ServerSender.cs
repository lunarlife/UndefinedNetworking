using UndefinedNetworking.Chats;
using UndefinedNetworking.Exceptions;

namespace UndefinedServer.Chats
{
    public class ServerSender : ISender, IRecipient
    {
        public static ServerSender Instance { get; private set; }
        public string Name => "Server";

        public ServerSender() => Instance = Instance is null ? this : throw new InstanceException("instance already created");

        static ServerSender()
        {
            _ = new ServerSender();
        }
        
        public void SendMessage(ChatMessage message)
        {
            Undefined.Logger.Info($"({message.Chat.DisplayName}) {message.Title} => {message.Text}");
        }
    }
}