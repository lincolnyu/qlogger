namespace QLogger.ConsoleHelpers
{
    public static class ArgsHelper
    {
        public static string GetSwitchValue(this string[] args, string sw)
        {
            for (var i = 0; i < args.Length; i++)
            {
                var x = args[i];
                if (x == sw)
                {
                    if (i + 1 < args.Length)
                    {
                        var t = args[i + 1];
                        if (t.StartsWith("-"))
                        {
                            return "";
                        }
                        return t;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            return null;
        }

    }
}
