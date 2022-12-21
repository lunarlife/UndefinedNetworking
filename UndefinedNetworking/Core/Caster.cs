using System;

namespace UndefinedNetworking.Core;

public class Caster<T, T1> 
{
    private readonly Func<T, T1> _func;

    public Caster(Func<T, T1> func)
    {
        _func = func;
    }

    public T1 Get(T cast) => _func(cast);
    public static implicit operator Caster<T, T1>(Func<T, T1> func) => new(func);
}