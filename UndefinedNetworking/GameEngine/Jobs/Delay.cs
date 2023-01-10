using UndefinedNetworking.Events.GameEngine;

namespace UndefinedNetworking.GameEngine.Jobs;

public sealed class Delay : JobInstruction
{
    private int _delayMs;
    private bool _isReady;
    public override bool IsReady => _isReady;

    public Delay(int delayMs)
    {
        _delayMs = delayMs;
    }
    public override void Tick(TickEventData e)
    {
        _delayMs -= (int)(e.DeltaTime * 1000f);
        if (_delayMs <= 0)
        {
            _isReady = true;
        }
    }
}