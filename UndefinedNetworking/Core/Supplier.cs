using System;

namespace UndefinedNetworking.Core;

public class Supplier<T>
{
    private readonly Func<T> _func;

    public Supplier(Func<T> func)
    {
        _func = func;
    }

    public T Get() => _func();
    public static implicit operator Supplier<T>(Func<T> func) => new(func);
}