using System.Linq;
using UndefinedNetworking.Chats;
using UndefinedNetworking.Commands;
using UndefinedServer;

namespace ServerPlugin.Commands
{
    public class PlayerParameter : ParameterType
    {
        public PlayerParameter(bool isRequired) : base(isRequired)
        {
        }

        public override string Title => "player_nickname";

        public override bool CompareWith(string parameter)
        {
            return UServer.CurrentGame.TryGetPlayer(parameter, out _);
        }

        public override string[]? GetCompletes(ISender player)
        {
            return UServer.CurrentGame.Players.Select(pl => pl.Nickname).ToArray();
        }
    }
    public class StringParameter : ParameterType, IRepeatable
    {
        public StringParameter(bool isRequired) : base(isRequired)
        {
        }

        public override string Title => "";

        public override bool CompareWith(string parameter)
        {
            return parameter.Length > 0;
        }

        public override string[]? GetCompletes(ISender player)
        {
            return new string[] { "Привет", "Как дела?", "Го в дискорд?", "Занят?" };
        }
    }
}
