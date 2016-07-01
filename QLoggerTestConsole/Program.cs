using System;
using QLogger.ConsoleHelpers;
using QLogger;
using QLogger.Logging;

namespace QLoggerTestConsole
{
    class Program
    {
        static void TestInplaceWriter()
        {
            InplaceWriter.Instance.RememberCursor();
            InplaceWriter.Instance.Write("The first message\n");
            Console.ReadKey(true);
            InplaceWriter.Instance.Write("The second message\n");
            Console.ReadKey(true);
            InplaceWriter.Instance.Write("A quite long sentence which also has\na second line");
            Console.ReadKey(true);
            InplaceWriter.Instance.Write("a short one\n");
        }

        static void TestStopwatch()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            System.Threading.Thread.Sleep(3000);
            stopwatch.End((watch, startUtc, endUtc) =>
            {
                Console.WriteLine($"Started at {startUtc}, ended at {endUtc}, duration {endUtc - startUtc}");
            });
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            TestStopwatch();
        }
    }
}
