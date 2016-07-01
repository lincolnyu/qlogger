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
            public string Relative { get; set; }
            public string Output { get; set; }
        }

        InputOutputPairs[] _samples = new InputOutputPairs[]
        {
            new InputOutputPairs
            {
                Base = @"C:\Program Files\Microsoft Visual Studio\v12",
                Relative = @"..\..\Common Files",
                Output = @"C:\Program Files\Common Files\"
            },
            new InputOutputPairs
            {
                Base = @"C:\Program Files\Microsoft Visual Studio\v12",
                Relative = @"\\..\..\.\Common Files",
                Output = @"C:\Program Files\Common Files\"
            },
            new InputOutputPairs
            {
                Base = @"C:",
                Relative = @"\\Program Files\Microsoft Visual Studio\..\..\.\Windows",
                Output = @"C:\Windows\"
            },
        };

        [TestMethod]
        public void TestWindowsPathHelper()
        {
            foreach (var sample in _samples)
            {
                var actual = sample.Base.ChangeDir(sample.Relative);
                var expected = sample.Output;
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
