using QLogger.FileSystemHelpers;
using System.Collections.Generic;
using System.IO;

namespace QLogger.Shell
{
    public class WindowsConsoleContext
    {
        public WindowsConsoleContext()
        {
            ResetCurrentDirectory();
        }

        public static WindowsConsoleContext Instance { get; } = new WindowsConsoleContext();

        public IDictionary<string, DirectoryInfo> CurrentDirectoryOfDrive { get; } = new Dictionary<string, DirectoryInfo>();

        public string CurrentDrive { get; set; }

        public DirectoryInfo CurrentDirectory
        {
            get
            {
                DirectoryInfo currDir;
                if (!CurrentDirectoryOfDrive.TryGetValue(CurrentDrive, out currDir))
                {
                    CurrentDirectoryOfDrive[CurrentDrive] = currDir = new DirectoryInfo(CurrentDrive + @":\");
                }
                return currDir;
            }
        }

        public bool SwichToDriveLazy(string drive)
        {
            var dl = drive.ToUpper().TrimEnd(':');
            var root = drive + @"\";
            if (!Directory.Exists(root))
            {
                return false;  
            }
            CurrentDrive = dl;
            return true;
        }

        public void ResetCurrentDirectory()
        {
            var dir =  Directory.GetCurrentDirectory();
            SetCurrentDriveCurrentDirectory(dir);
        }

        public bool SetDirectory(string dir)
        {
            if (Directory.Exists(dir))
            {
                SetCurrentDriveCurrentDirectory(dir);
                return true;
            }
            return false;
        }

        private void SetCurrentDriveCurrentDirectory(string dir)
        {
            CurrentDrive = dir.GetDriveLetters().ToUpper();
            CurrentDirectoryOfDrive[CurrentDrive] = new DirectoryInfo(dir);
        }
    }
}
