namespace UndefinedNetworking.Core
{
    public abstract class Wait
    {
        public abstract string CurrentState { get; }
        public abstract int Precent { get; }
    }
}