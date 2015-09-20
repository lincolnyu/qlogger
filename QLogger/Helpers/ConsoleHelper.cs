using System;

namespace QLogger.Helpers
{
    public static class ConsoleHelper
    {
        #region Methods

        public static int InplaceWrite(this int backwards, string msg)
        {
            for (var i = 0; i < backwards; i++)
            {
                Console.Out.Write('\b');
            }
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
            for (var i = 0; i < backwards; i++)
            {
                Console.Out.Write('\b');
            }
            Console.Out.WriteLine(msg);
        }

        public static void InplaceWriteLine(this int backwards,
            string format, params object[] args)
        {
            for (var i = 0; i < backwards; i++)
            {
                Console.Out.Write('\b');
            }
            Console.Out.WriteLine(format, args);
        }

        #endregion
    }
}
