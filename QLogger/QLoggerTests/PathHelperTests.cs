using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLogger.FileSystemHelpers;

namespace QLoggerTests
{
    [TestClass]
    public class PathHelperTests
    {
        class InputOutputPairs
        {
            public string Base { get; set; }
            public string ChangeTo { get; set; }
            public string Output { get; set; }
        }

        InputOutputPairs[] _samples = new InputOutputPairs[]
        {
            new InputOutputPairs
            {
                Base = @"C:\Program Files\Microsoft Visual Studio\v12",
                ChangeTo = @"..\..\Common Files",
                Output = @"C:\Program Files\Common Files\"
            },
            new InputOutputPairs
            {
                Base = @"C:\Program Files\Microsoft Visual Studio\v12",
                ChangeTo = @"..\\..\.\Common Files",
                Output = @"C:\Program Files\Common Files\"
            },
            new InputOutputPairs
            {
                Base = @"C:",
                ChangeTo = @"\\Program Files\Microsoft Visual Studio\..\..\.\Windows",
                Output = @"C:\Windows\"
            },
            new InputOutputPairs
            {
                Base = @"C:\Program Files\Microsoft Visual Studio\v12",
                ChangeTo = @"\\Windows\System32",
                Output = @"C:\Windows\System32\"
            },
            new InputOutputPairs // invalid change-to input
            {
                Base = @"C:\Program Files\Microsoft Visual Studio\v12",
                ChangeTo = @"\\..\..\Windows\System32",
                Output = @"C:\Windows\System32\"
            },
            new InputOutputPairs // invalid input
            {
                Base = @"C:",
                ChangeTo = @"\\..\..\Windows\System32",
                Output = @"C:\Windows\System32\"
            },
            new InputOutputPairs
            {
                Base = @"C:",
                ChangeTo = @"\",
                Output = @"C:\"
            },
            new InputOutputPairs
            {
                Base = @"C:\Program Files\Microsoft Visual Studio\v12",
                ChangeTo = @"\",
                Output = @"C:\"
            }
        };

        [TestMethod]
        public void TestWindowsPathHelper()
        {
            foreach (var sample in _samples)
            {
                var actual = sample.Base.ChangeDir(sample.ChangeTo);
                var expected = sample.Output;
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
