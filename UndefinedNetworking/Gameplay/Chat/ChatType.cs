using UndefinedNetworking.Chats;
using Utils.Enums;

namespace UndefinedNetworking.Gameplay.Chat
{
    public abstract class ChatType : EnumType
    {
        public abstract string DisplayName { get; }
        public abstract bool CanUseCommands { get; }
        public abstract bool CanWriteMessages { get; }
        public abstract void Execute(ISender sender, string message);
    }
}