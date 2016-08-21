namespace QLogger.ConsoleHelpers
{
    public static class ParallelArgsHelper
    {
        public static int GetMaxDegreeOfParallelism(this string[] args, string parallelSwitch = "-p")
        {
            var parallelarg = args.GetSwitchValue(parallelSwitch);
            if (parallelarg == null) return 1;
            int parallel;
            int.TryParse(parallelarg, out parallel);
            return parallel;
        }
    }
}
