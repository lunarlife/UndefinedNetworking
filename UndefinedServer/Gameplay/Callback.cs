using System;

namespace UndefinedServer.Gameplay
{
    public class Callback
    {
        private readonly Action _action;

        public Callback(Action action)
        {
            _action = action;
        }

        public void Invoke() => _action.Invoke();

        public static implicit operator Callback(Action action) => new(action);
    }
    public class Callback<T>
    {
        private readonly Func<T> _func;

        public Callback(Func<T> func)
        {
            _func = func;
        }

        public void Invoke()
        {
            _func.Invoke();
        }
        public static implicit operator Callback<T>(Func<T> func) => new(func);
    }
}