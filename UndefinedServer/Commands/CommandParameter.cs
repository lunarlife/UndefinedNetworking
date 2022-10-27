using UndefinedNetworking.Commands;

namespace UndefinedServer.Commands
{
    public sealed class CommandParameter
    {
        public ParameterType Type { get; }
        private string _arg;

        internal CommandParameter(ParameterType type, string arg)
        {
            Type = type;
            _arg = arg;
        }

        public T? Cast<T>() where T : class => Type.FromString(_arg) as T;
        public static implicit operator string(CommandParameter parameter) => parameter._arg;
    }
}