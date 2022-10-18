namespace UndefinedServer.Commands
{
    public class CommandParameter
    {
        private string _arg;

        public CommandParameter(string arg)
        {
            _arg = arg;
        }

        public static implicit operator string(CommandParameter parameter) => parameter._arg;
    }
}