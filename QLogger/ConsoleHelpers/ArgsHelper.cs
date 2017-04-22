using System.Collections.Generic;

namespace QLogger.ConsoleHelpers
{
    public static class ArgsHelper
    {
        public static string GetSwitchValue(this IList<string> args, string sw)
        {
            for (var i = 0; i < args.Count; i++)
            {
                var x = args[i];
                if (x == sw)
                {
                    if (i + 1 < args.Count)
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

        public static string GetSwitchValueNoSpace(this IList<string> args, string sw)
        {
            for (var i = 0; i < args.Count; i++)
            {
                var x = args[i];
                if (x.StartsWith(sw))
                {
                    return x.Substring(sw.Length);
                }
            }
            return null;
        }

        public static IEnumerable<string> ParseArgs(this string argsLine)
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
                            if (i < argsLine.Length - 1 && argsLine[i+1] == '"')
                            {
                                currArg += c;
                                i++;
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
            if (currArg != "")
            {
                yield return currArg;
            }
        }

        /// <summary>
        ///  Parse switch value as int
        /// </summary>
        /// <param name="args">The arg list</param>
        /// <param name="sw">The switch</param>
        /// <param name="absenceValue">Value to return when the switch is absent</param>
        /// <param name="defaultValue">Value to return when the switch is present but value is not provided or is invalid</param>
        /// <returns>The value</returns>
        public static int GetSwitchValueAsInt(this IList<string> args, string sw, int absenceValue = 0, int defaultValue = 0)
        {
            var res = args.GetSwitchValueAsIntOpt(sw, defaultValue);
            if (res == null) return absenceValue;
            return res.Value;
        }

        /// <summary>
        ///  Parse switch value as long
        /// </summary>
        /// <param name="args">The arg list</param>
        /// <param name="sw">The switch</param>
        /// <param name="absenceValue">Value to return when the switch is absent</param>
        /// <param name="defaultValue">Value to return when the switch is present but value is not provided or is invalid</param>
        /// <returns>The value</returns>
        /// <returns>The value</returns>
        public static long GetSwitchValueAsLong(this IList<string> args, string sw, int absenceValue = 0, int defaultValue = 0)
        {
            var res = args.GetSwitchValueAsLongOpt(sw, defaultValue);
            if (res == null) return absenceValue;
            return res.Value;
        }

        /// <summary>
        ///  Parse switch value as nullable int, returning null if absent
        /// </summary>
        /// <param name="args">The arg list</param>
        /// <param name="sw">The switch</param>
        /// <param name="defaultValue">Value to return when the switch is present but value is not provided or is invalid</param>
        /// <returns>The value</returns>
        public static int? GetSwitchValueAsIntOpt(this IList<string> args, string sw, int? defaultValue = 0)
        {
            var str = args.GetSwitchValue(sw);
            if (str == null) return null;
            if (string.IsNullOrWhiteSpace(str)) return defaultValue;
            if (!int.TryParse(str, out int val))
            {
                return defaultValue;
            }
            return val;
        }

        /// <summary>
        ///  Parse switch value as nullable long, returning null if absent
        /// </summary>
        /// <param name="args">The arg list</param>
        /// <param name="sw">The switch</param>
        /// <param name="defaultValue">Value to return when the switch is present but value is not provided or is invalid</param>
        /// <returns>The value</returns>
        public static long? GetSwitchValueAsLongOpt(this IList<string> args, string sw, long? defaultValue = 0)
        {
            var str = args.GetSwitchValue(sw);
            if (str == null) return null;
            if (string.IsNullOrWhiteSpace(str)) return defaultValue;
            if (!long.TryParse(str, out long val))
            {
                return defaultValue;
            }
            return val;
        }

        /// <summary>
        ///  Parse switch value as double
        /// </summary>
        /// <param name="args">The arg list</param>
        /// <param name="sw">The switch</param>
        /// <param name="absenceValue">Value to return when the switch is absent</param>
        /// <param name="defaultValue">Value to return when the switch is present but value is not provided or is invalid</param>
        /// <returns>The value</returns>
        public static double GetSwitchValueAsDouble(this IList<string> args, string sw, double absenceValue = 0, int defaultValue = 0)
        {
            var res = args.GetSwitchValueAsDoubleOpt(sw, defaultValue);
            if (res == null) return absenceValue;
            return res.Value;
        }
        /// <summary>
        ///  Parse switch value as nullable double, returning null if absent
        /// </summary>
        /// <param name="args">The arg list</param>
        /// <param name="sw">The switch</param>
        /// <param name="defaultValue">Value to return when the switch is present but value is not provided or is invalid</param>
        /// <returns>The value</returns>
        public static double? GetSwitchValueAsDoubleOpt(this IList<string> args, string sw, double? defaultValue = 0)
        {
            var str = args.GetSwitchValue(sw);
            if (str == null) return null;
            if (string.IsNullOrWhiteSpace(str)) return defaultValue;
            if (!double.TryParse(str, out double val))
            {
                return defaultValue;
            }
            return val;
        }
    }
}
