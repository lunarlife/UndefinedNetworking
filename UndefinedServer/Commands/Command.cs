using UndefinedNetworking.Chats;
using UndefinedNetworking.Commands;
using UndefinedNetworking.Gameplay.Chat;
using UndefinedServer.Chats;
using Utils.Enums;

namespace UndefinedServer.Commands
{
    public abstract class Command : EnumType, ICommand
    {
        public abstract string Prefix { get; }
        public abstract string Description { get; }
        public abstract ParameterType[]? Parameters { get; }
        public abstract void Execute(ISender sender, CommandParameter[]? args, ChatType type);

        public virtual void OnFailArgumentsLength(ISender sender, ChatType type)
        {
            if (sender is IRecipient res) res.SendMessage(new ChatMessage(ServerSender.Instance, $"failed to usage command {Prefix}", type));
        }
    }
}