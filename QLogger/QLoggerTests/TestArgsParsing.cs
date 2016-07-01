using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLogger.ConsoleHelpers;
using System.Linq;

namespace QLoggerTests
{
    [TestClass]
    public class TestArgsParsing
    {
        class InputOutputPairs
        {
            public string Input { get; set; }
            public string[] Output { get; set; }
        }

        InputOutputPairs[] _sampels = new InputOutputPairs[]
        {
            new InputOutputPairs
            {
                Input = @"-s c:\windows\command.com",
                Output = new [] { "-s", @"c:\windows\command.com" }
            },
            new InputOutputPairs
            {
                Input = "-s \"c:\\program files\\microsoft\"",
                Output = new [] { "-s", @"c:\program files\microsoft" }
            },
            new InputOutputPairs
            {
                Input = "-s \"c:\\program files\\micro\"\"soft\"",
                Output = new [] { "-s", "c:\\program files\\micro\"soft" }
            },
            new InputOutputPairs
            {
                Input = "-s \"c:\\program files\\\"\"micro\"\"\"\"soft\"\"\"",
                Output = new [] { "-s", "c:\\program files\\\"micro\"\"soft\"" }
            },
        };

        [TestMethod]
        public void TestMethod1()
        {
            foreach (var sample in _sampels)
            {
                var actual = sample.Input.ParseArgs().ToArray();
                var expected = sample.Output;
                Assert.AreEqual(expected.Length, actual.Length);
                for (var i = 0; i < expected.Length; i++)
                {
                    Assert.AreEqual(expected[i], actual[i]);
                }
            }
        }
    }
}
