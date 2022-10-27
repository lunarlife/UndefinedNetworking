using System;
using System.Collections.Generic;
using System.Linq;

namespace UndefinedNetworking.GameEngine;

public static class EnumExtensions
{
    public static IEnumerable<T> GetUniqueFlags<T>(this T flags) where T : Enum => Enum.GetValues(flags.GetType()).Cast<T>().Where(value => flags.HasFlag(value));
}