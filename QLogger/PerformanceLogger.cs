using System;
using System.Collections.Generic;
using static QLogger.ConsoleHelpers.WriteMethods;

namespace QLogger
{
    public class PerformanceLogger : Logger
    {
        #region Properties

        public Stack<DateTime> StartTimes { get; private set; } = new Stack<DateTime>();

        #endregion

        #region Methods

        public void WriteStart(StaticWriteMethod write, string msg)
        {
            write(msg);

            StartTimes.Push(DateTime.UtcNow);
        }

        public void WriteStartFormat(StaticWriteFormatMethod write, string fmt, params object[] args)
        {
            write(fmt, args);

            StartTimes.Push(DateTime.UtcNow);
        }

        public void WriteEnd(StaticWriteMethod write, string msg)
        {
            var st = StartTimes.Pop();
            var et = DateTime.UtcNow;
            var elapsed = et - st;

            var s = string.Format("{0} ({1} secs elapsed)", msg, elapsed.TotalSeconds);
            write(s);
        }

        public void WriteEndFormat(StaticWriteFormatMethod write, string fmt, params object[] args)
        {
            var s = string.Format(fmt, args);
            var st = StartTimes.Pop();
            var et = DateTime.UtcNow;
            var elapsed = et - st;

            s = string.Format("{0} ({1} secs elapsed)", s, elapsed.TotalSeconds);
            write(s);
        }

        public void WriteStart(string msg)
        {
            WriteStart(Write, msg);
        }

        public void WriteLineStart(string msg)
        {
            WriteStart(WriteLine, msg);
        }

        public void WriteStart(string fmt,  params object[] args)
        {
            WriteStartFormat(Write, fmt, args);
        }

        public void WriteLineStart(string fmt, params object[] args)
        {
            WriteStartFormat(WriteLine, fmt, args);
        }

        public void WriteEnd(string msg)
        {
            WriteEnd(Write, msg);
        }

        public void WriteLineEnd(string msg)
        {
            WriteEnd(WriteLine, msg);
        }

        public void WriteEnd(string fmt, params object[] args)
        {
            WriteEndFormat(Write, fmt, args);
        }

        public void WriteLineEnd(string fmt, params object[] args)
        {
            WriteEndFormat(WriteLine, fmt, args);
        }

        #endregion
    }
}
