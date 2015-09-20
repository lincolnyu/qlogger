using System;

namespace QLogger.Helpers
{
    public static class ConsoleHelper
    {
        #region Methods

        private static string EraseAndExtend(int backwards, string msg)
        {
            for (var i = 0; i < backwards; i++)
            {
                Console.Out.Write('\b');
            }
            var diff = backwards - msg.Length;
            if (diff > 0)
            {
                var c = new string(' ', diff);
                msg += c;
            }
            return msg;
        }


        public static int InplaceWrite(this int backwards, string msg)
        {
            msg = EraseAndExtend(backwards, msg);
            Console.Out.Write(msg);
            return msg.Length;
        }

        public static int InplaceWrite(this int backwards, string format, params object[] args)
        {
            var s = string.Format(format, args);
            return InplaceWrite(backwards, s);
        }

        public static void InplaceWriteLine(this int backwards, string msg)
        {
            msg = EraseAndExtend(backwards, msg);
            Console.Out.WriteLine(msg);
        }

        public static void InplaceWriteLine(this int backwards,
            string format, params object[] args)
        {
            var s = string.Format(format, args);
            backwards.InplaceWriteLine(s);
        }

        #endregion
    }
}
