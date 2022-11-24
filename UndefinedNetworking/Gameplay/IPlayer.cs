using UndefinedNetworking.Chats;
using UndefinedNetworking.GameEngine.UI;

namespace UndefinedNetworking.Gameplay
{
    public interface IPlayer : ISender, IRecipient, IUIViewer
    {
        public string Nickname { get; }
    }
}