using System;
using QLogger.ConsoleHelpers;
using QLogger;

namespace QLoggerTest
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

        static void TestPerfLog()
        {
            var perflog = new PerformanceLogger();
            perflog.Writers.Add(InplaceWriter.Instance);
            perflog.WriteStart("wait started");
            InplaceWriter.Instance.RememberCursor();
            InplaceWriter.Instance.WriteLine();
            InplaceWriter.Instance.RememberCursor();
            System.Threading.Thread.Sleep(3000);
            perflog.WriteEnd("wait finished");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            TestPerfLog();
        }
    }
}
