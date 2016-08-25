using System;

namespace QLogger.ConsoleHelpers
{
    public static class ParallelArgsHelper
    {
        public static int GetMaxDegreeOfParallelism(this string[] args, string parallelSwitch = "-p")
        => Math.Max(1, args.GetSwitchValueAsInt(parallelSwitch, 1, int.MaxValue));
    }
}
