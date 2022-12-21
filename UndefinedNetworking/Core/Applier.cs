using System;

namespace UndefinedNetworking.Core;

public class Applier<T>
{
    private readonly Action<T> _action;

    public Applier(Action<T> action)
    {
        _action = action;
    }

    public void Invoke(T value) => _action(value);
}