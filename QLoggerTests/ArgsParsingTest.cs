using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLogger.ConsoleHelpers;
using System.Linq;

namespace QLoggerTests
{
    [TestClass]
    public class ArgsParsingTest
    {
        class InputOutputPairs
        {
            public string Input { get; set; }
            public string[] Output { get; set; }
        }

        InputOutputPairs[] _samples = new InputOutputPairs[]
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
        public void TestArgsParsing()
        {
            foreach (var sample in _samples)
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
