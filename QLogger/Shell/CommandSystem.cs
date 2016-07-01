using System.IO;

namespace QLogger.Shell
{
    public class CommandSystem
    {
        public enum Results
        {
            Success = 0,
            BadCommand,
            InvalidArgs,
        }

        public readonly static string[] ResultMessages = new string[]
        {
            "Command successfully executed",
            "Bad command",
            "Invalid command arguments"
        };
            

        public CommandSystem(ConsoleContext cc)
        {
            ConsoleContext = cc;
        }

        public ConsoleContext ConsoleContext { get; }

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
            switch (cmd)
            {
                case "cd":
                    return PerformCd(args);
            }
            return Results.BadCommand;
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
            if (Path.IsPathRooted(dir))
            {
                ConsoleContext.SetDirectory(dir);
            }
            else
            {
                var path = Path.Combine(ConsoleContext.CurrentDirectory.FullName, dir);
                path.TrimEnd('.');
                ConsoleContext.SetDirectory(path);
            }
            return Results.Success;
        }
    }
}
