using System.Linq;
using UndefinedNetworking.Chats;
using UndefinedNetworking.Commands;

namespace UndefinedServer.Commands.ParameterTypes;

public class PlayerNicknameParameterType : ParameterType
{
    public PlayerNicknameParameterType(bool isRequired, bool isRepeatable) : base(isRequired, isRepeatable)
    {
    }

    public override string Title => "Player nickname";
    public override string[] GetCompletes(ISender player) => Undefined.CurrentGame.Players.Select(t => t.SenderName).ToArray();

    public override bool CompareWith(string parameter) => Undefined.CurrentGame.TryGetPlayer(parameter, out _);
    public override object? FromString(string st) => Undefined.CurrentGame.TryGetPlayer(st, out var player) ? player : null;
}