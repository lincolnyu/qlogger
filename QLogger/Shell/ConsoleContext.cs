using System.IO;

namespace QLogger.Shell
{
    public class ConsoleContext
    {
        public static ConsoleContext Instance { get; } = new ConsoleContext();

        public DirectoryInfo CurrentDirectory { get; set; }

        public void ResetCurrentDirectory()
        {
            var path =  Directory.GetCurrentDirectory();
            CurrentDirectory = new DirectoryInfo(path);
        }

        public void SetDirectory(string dir)
        {
            CurrentDirectory = new DirectoryInfo(dir);
        }
    }
}
