using UndefinedNetworking.Chats;
using UndefinedNetworking.Commands;

namespace UndefinedServer.Commands.ParameterTypes;

public class StringParameterType : ParameterType
{
    public override string Title => "String";
    public override string[]? GetCompletes(ISender player) => null;
    public override bool CompareWith(string parameter) => true;


    public StringParameterType(bool isRequired, bool isRepeatable) : base(isRequired, isRepeatable)
    {
    }
    public override object? FromString(string st) => st;
}