using System.Text;

namespace QLogger.FileSystemHelpers
{
    public static class WindowsPathHelper
    {
        private enum DotTypes
        {
            None,
            SlashDotSlash,
            SlashDotDotSlash,
            SlashDotEnd,
            SlashDotDotEnd,
            BeginDotDotSlash,
            BeginDotSlash,
            BeginDotEnd,
            BeginDotDotEnd
        }

        public static string NormalizeDir(this string dir)
        {
            dir = dir.Trim();
            if (dir.Length == 0) return dir;
            var sb = new StringBuilder();
            for (var i = 0; i < dir.Length; i++)
            {
                var c = dir[i];
                if (c == '\\' && i > 0 && dir[i - 1] == '\\')
                {
                    continue;
                }
                var dotType = GetDotType(dir, i);
                switch (dotType)
                {
                    case DotTypes.SlashDotDotSlash:
                        DirUp(sb);
                        i += 2;
                        break;
                    case DotTypes.SlashDotDotEnd:
                        DirUp(sb);
                        i++;
                        break;
                    case DotTypes.SlashDotSlash:
                        i++;
                        break;
                    case DotTypes.SlashDotEnd:
                        break;
                    case DotTypes.BeginDotDotSlash:
                        sb.Append("..\\");
                        i += 2;
                        break;
                    case DotTypes.BeginDotSlash:
                        sb.Append(".\\");
                        i++;
                        break;
                    case DotTypes.BeginDotDotEnd:
                        sb.Append("..\\");
                        i++;
                        break;
                    case DotTypes.BeginDotEnd:
                    case DotTypes.None:
                        sb.Append(c);
                        break;
                }
            }
            if (sb[sb.Length - 1] !=  '\\')
            {
                sb.Append('\\');
            }
            return sb.ToString();
        }

        private static DotTypes GetDotType(string s, int index)
        {
            var c = s[index];
            if (c != '.') return 0;
            if (index > 0)
            {
                var cm1 = s[index - 1];
                if (cm1 == '\\')
                {
                    if (index + 1 < s.Length)
                    {
                        var cp1 = s[index + 1];
                        if (cp1 == '\\')
                        {
                            return DotTypes.SlashDotSlash;
                        }
                        if (cp1 == '.')
                        {
                            if (index + 2 < s.Length)
                            {
                                var cp2 = s[index + 2];
                                if (cp2 == '\\')
                                {
                                    return DotTypes.SlashDotDotSlash; // \..\
                                }
                                return DotTypes.None; // \..?
                            }
                            return DotTypes.SlashDotDotEnd; // \..
                        }
                        return DotTypes.None;   // \.? could be hidden file
                    }
                    return DotTypes.SlashDotEnd; // \.
                }
                return DotTypes.None;   // ?.
            }
            // relative dir
            if (index + 1 < s.Length)
            {
                var cp1 = s[index + 1];
                if (cp1 == '\\')
                {
                    return DotTypes.BeginDotSlash; // .\
                }
                if (cp1 == '.')
                {
                    if (index + 2 < s.Length)
                    {
                        var cp2 = s[index + 2];
                        if (cp2 == '\\')
                        {
                            return DotTypes.BeginDotDotSlash;   // ..\
                        }
                        return DotTypes.None; // ..?
                    }
                    return DotTypes.BeginDotDotEnd; // ..
                }
                return DotTypes.None; // .?
            }
            return DotTypes.BeginDotEnd; // .
        }

        private static void DirUp(StringBuilder sb)
        {
            // no exceptions thrown or errors reported
            var len = sb.Length;
            var i = len - 1;
            if (i < 0) return;
            if (sb[i] == '\\') i--;
            for (; i >= 0 && sb[i] != '\\'; i--)
            {
            }
            sb.Remove(i + 1, len - i - 1); // excluding the base '\\'
        }

        public static string ChangeDir(this string currentDir, string relativeDir)
        {
            var slashNeeded = !currentDir.EndsWith("\\") && !relativeDir.StartsWith("\\");
            var combined = slashNeeded ? currentDir + "\\" + relativeDir : currentDir + relativeDir;
            return combined.NormalizeDir();
        }
    }
}
