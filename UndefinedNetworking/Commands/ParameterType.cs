using UndefinedNetworking.Chats;
using UndefinedNetworking.Gameplay.Chat;

namespace UndefinedNetworking.Commands
{
    public abstract class ParameterType
    {
        public abstract string Title { get; } 
        public bool IsRequired { get; }
        public bool IsRepeatable { get; }

        public ParameterType(bool isRequired, bool isRepeatable)
        {
            IsRequired = isRequired;
            IsRepeatable = isRepeatable;
        }

        public abstract string[]? GetCompletes(ISender player);
        public abstract bool CompareWith(string parameter);

        public virtual void OnFailedUsage(ISender sender, ChatType type)
        {
            if(sender is IRecipient res)
                res.SendMessage(new ChatMessage("Error", "Failed to write parameter", type));
        }

        public abstract object? FromString(string st);
    }
}