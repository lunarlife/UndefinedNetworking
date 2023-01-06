using System.Collections.Generic;
using UndefinedNetworking.Events.GameEngine;
using Utils.Events;

namespace UndefinedNetworking.GameEngine.Jobs;

public static class JobSystem
{
    private static readonly List<IEnumerator<JobInstruction?>> Jobs = new();
    
    static JobSystem()
    {
        EventManager.RegisterStaticEvents(typeof(JobSystem));
    }
    public static void Run(IEnumerable<JobInstruction?> coroutine)
    {
        Jobs.Add(coroutine.GetEnumerator());
    }

    [EventHandler]
    private static void Tick(TickEvent e)
    {
        foreach (var job in Jobs)
        {
            if (job.Current is not { } instruction)
            {
                job.MoveNext();
                continue;
            }
            instruction.Tick(e);
            if (instruction.IsReady) job.MoveNext();
        }
    }
}