namespace UndefinedNetworking.GameEngine.Output;

public static class PlayerValue
{
    private static int _currentValue;
    private static object NewValue => $"![{_currentValue++}]";
    
    public static object FpsAvg { get; } = NewValue;
    public static object FpsMin { get; } = NewValue;
    public static object FpsMax { get; } = NewValue;
    
    
}