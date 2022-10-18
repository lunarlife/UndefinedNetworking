namespace UndefinedNetworking.Commands
{
    public interface ICommand
    {
        public string Prefix { get; }
        public string Description { get; }
        public ParameterType[]? Parameters { get; }
        
    }
}