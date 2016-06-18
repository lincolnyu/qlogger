using System.IO;

namespace QLogger.ConsoleHelpers
{
    public static class WriteMethods
    {
        public delegate void StaticWriteMethod(string msg);

        public delegate void StaticWriteFormatMethod(string format, params object[] args);

        public delegate void TextWriteMethod(TextWriter tw, string msg);

        public delegate void TextWriteFormatMethod(TextWriter tw, string fmt, params object[] args);

        public delegate void InplaceWriteMethod(InplaceWriter tw, string msg);

        public delegate void InplaceWriteFormatMethod(InplaceWriter tw, string fmt, params object[] args);

        public static void TextWrite(TextWriter tw, string msg)
        {
            tw.Write(msg);
        }

        public static void TextWriteLine(TextWriter tw, string msg)
        {
            tw.WriteLine(msg);
        }

        public static void TextWrite(TextWriter tw, string fmt, params object[] args)
        {
            tw.Write(fmt, args);
        }

        public static void TextWriteLine(TextWriter tw, string fmt, params object[] args)
        {
            tw.WriteLine(fmt, args);
        }

        public static void InplaceWrite(InplaceWriter iw, string msg)
        {
            iw.Write(msg);
        }

        public static void InplaceWriteLine(InplaceWriter iw, string msg)
        {
            iw.WriteLine(msg);
        }

        public static void InplaceWrite(InplaceWriter iw, string fmt, params object[] args)
        {
            iw.WriteFormat(true, fmt, args);
        }

        public static void InplaceWriteLine(InplaceWriter iw, string fmt, params object[] args)
        {
            iw.WriteLineFormat(true, fmt, args);
        }
    }
}
