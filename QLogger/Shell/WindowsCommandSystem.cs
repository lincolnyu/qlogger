using System;
using QLogger.FileSystemHelpers;

namespace QLogger.Shell
{
    public class WindowsCommandSystem
    {
        public enum Results
        {
            Success = 0,
            BadCommand,
            InvalidArgs,
            WrongDir,
            WrongDrive
        }

        public readonly static string[] ResultMessages = new string[]
        {
            "Command successfully executed",
            "Bad command",
            "Invalid command arguments",
            "Directory doesn't exist"
        };
            
        public WindowsCommandSystem(WindowsConsoleContext cc)
        {
            ConsoleContext = cc;
        }

        public WindowsConsoleContext ConsoleContext { get; }

        public static string ResultToString(Results result)
        {
            return ResultMessages[(int)result];
        }

        public Results ExecuteCommand(string[] args)
        {
            if (args.Length == 0)
            {
                return Results.Success; // empty command
            }
            var cmd = args[0].ToLower();
            if (cmd.IsDriveIndicator())
            {
                return SwitchToDrive(cmd);
            }
            switch (cmd)
            {
                case "cd":
                    return PerformCd(args);
            }
            return Results.BadCommand;
        }

        private Results SwitchToDrive(string drive)
        {
            return ConsoleContext.SwichToDriveLazy(drive) ? Results.Success : Results.WrongDrive;
        }

        private Results PerformCd(string[] args)
        {
            if (args.Length == 1)
            {
                ConsoleContext.ResetCurrentDirectory();
                return Results.Success;
            }
            if (args.Length > 2)
            {
                return Results.InvalidArgs;
            }
            var dir = args[1];
            if (!dir.IsAbsoluteDir())
            {
                var currDir = ConsoleContext.CurrentDirectory.FullName;
                dir = currDir.ChangeDir(dir);
            }
            else
            {
                if (dir.EndsWith(":"))
                {
                    return Results.Success;
                }
                else
                {
                    dir = dir.NormalizeDir();
                }
            }
            return ConsoleContext.SetDirectory(dir)? Results.Success : Results.WrongDir;
        }
    }
}
