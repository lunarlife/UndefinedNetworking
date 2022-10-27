using UndefinedNetworking.Chats;
using UndefinedNetworking.Commands;

namespace UndefinedServer.Commands.ParameterTypes;

public class IntParameterType : ParameterType
{
    public IntParameterType(bool isRequired, bool isRepeatable) : base(isRequired, isRepeatable)
    {
    }

    public override string Title => "Number";
    public override string[]? GetCompletes(ISender player)
    {
        return null;
    }

    public override bool CompareWith(string parameter) => int.TryParse(parameter, out _);
    public override object? FromString(string st) => int.TryParse(st, out var o) ? o : null;
}