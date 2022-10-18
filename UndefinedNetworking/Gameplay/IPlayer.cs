using UndefinedNetworking.Chats;

namespace UndefinedNetworking.Gameplay
{
    public interface IPlayer : ISender, IRecipient
    {
        public string Nickname { get; }
    }
}