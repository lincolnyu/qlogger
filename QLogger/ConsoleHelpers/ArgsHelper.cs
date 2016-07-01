using System.Collections.Generic;
using System.Text.RegularExpressions;

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

        public static IEnumerable<string> ConvertStringToArgs(this string argsLine)
        {
            argsLine = argsLine.Trim();
            var inQuote= false;
            var currArg = "";
            for (var i = 0; i < argsLine.Length; i++)
            {
                var c = argsLine[i];
                if (!inQuote && char.IsWhiteSpace(c))
                {
                    yield return currArg;
                    currArg = "";
                }
                else
                {
                    if (c == '"')
                    {
                        if (!inQuote)
                        {
                            inQuote = true;
                        }
                        else
                        {
                            // look ahead
                            if (i < argsLine.Length - 1 && argsLine[i] == '"')
                            {
                                currArg += c;
                            }
                            else
                            {
                                inQuote = false;
                            }
                        }
                    }
                    else
                    {
                        currArg += c;
                    }
                }
            }
        }
    }
}
