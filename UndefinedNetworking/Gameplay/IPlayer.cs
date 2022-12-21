using UndefinedNetworking.Chats;
using UndefinedNetworking.GameEngine.Scenes;

namespace UndefinedNetworking.Gameplay
{
    public interface IPlayer : ISender, IRecipient, ISceneViewer
    {
        public string Nickname { get; }
    }
}