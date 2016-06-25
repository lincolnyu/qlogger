using System;
using static QLogger.ConsoleHelpers.WriteMethods;

namespace QLogger.ConsoleHelpers
{

    public class InplaceWriter
    {
        #region Delegates
        
        private delegate void InternalWriteMethod(StaticWriteMethod write, string msg);

        private delegate void InternalWriteFormatMethod(StaticWriteFormatMethod write, string format, params object[] args);

        #endregion

        #region Properties

        // by default print at most 5 times every sec
        public readonly static TimeSpan DefaultRefreshInterval = TimeSpan.FromMilliseconds(200);

        public static InplaceWriter Instance { get; } = new InplaceWriter();

        public static int LineLength => Console.BufferWidth;

        public int LastCursorLeft { get; private set; }

        public int LastCursorTop { get; private set; }

        public DateTime LastPrintTime { get; private set; }

        public TimeSpan MinRefreshInterval { get; set; } = DefaultRefreshInterval;

        #endregion

        #region Methods

        public void RememberCursor()
        {
            LastCursorTop = Console.CursorTop;
            LastCursorLeft = Console.CursorLeft;
        }

        public void RestoreCursor()
        {
            Console.CursorTop = LastCursorTop;
            Console.CursorLeft = LastCursorLeft;
        }

        public void WriteNoPadding(StaticWriteMethod write, string s)
        {
            RestoreCursor();
            write(s);
        }

        public void WriteFormatNoPadding(StaticWriteFormatMethod write, string format, params object[] args)
        {
            lock(this)
            {
                RestoreCursor();
                write(format, args);
            }
        }

        public void WritePadding(StaticWriteMethod write, string s)
        {
            lock(this)
            {
                var x = Console.CursorLeft;
                var y = Console.CursorTop;
                RestoreCursor();
                if (Console.CursorTop < y || Console.CursorTop == y && Console.CursorLeft < x)
                {
                    var paddingLen = (y - Console.CursorTop) * LineLength
                        + (x - Console.CursorLeft);
                    var padding = new string(' ', paddingLen);
                    Console.Write(padding);
                }
                RestoreCursor();
                write(s);
            }
        }

        public void WriteFormatPadding(StaticWriteFormatMethod write, string format, params object[] args)
        {
            lock(this)
            {
                var x = Console.CursorLeft;
                var y = Console.CursorTop;
                RestoreCursor();
                if (Console.CursorTop < y || Console.CursorTop == y && Console.CursorLeft < x)
                {
                    var paddingLen = (y - Console.CursorTop) * LineLength
                        + (x - Console.CursorLeft);
                    var padding = new string(' ', paddingLen);
                    Console.Write(padding);
                }
                RestoreCursor();
                write(format, args);
            }
        }

        private void Write(StaticWriteMethod write, bool padding, string s)
        {
            var internalWrite = padding ? (InternalWriteMethod)WritePadding : WriteNoPadding;
            internalWrite(write, s);
        }

        private void WriteFormat(StaticWriteFormatMethod write, bool padding, string format, params object[] args)
        {
            var internalWrite = padding ? (InternalWriteFormatMethod)WriteFormatPadding : WriteFormatNoPadding;
            internalWrite(write, format, args);
        }

        public void Write(string s, bool padding = true)
        {
            Write(Console.Write, padding, s);
        }

        public void WriteLine(string s="", bool padding = true)
        {
            Write(Console.WriteLine, padding, s);
        }

        public void WriteFormat(bool padding, string format, params object[] args)
        {
            WriteFormat(Console.Write, padding, format, args);
        }

        public void WriteLineFormat(bool padding, string format, params object[] args)
        {
            WriteFormat(Console.WriteLine, padding, format, args);
        }
        
        public void UpdateLastRefreshTime()
        {
            LastPrintTime = DateTime.UtcNow;
        }

        public bool CanRefreshNow()
        {
            var dur = DateTime.UtcNow - LastPrintTime;
            return dur >= MinRefreshInterval;
        }

        #endregion
    }
}
