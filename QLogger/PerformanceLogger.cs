using System;
using System.Collections.Generic;

namespace QLogger
{
    public class PerformanceLogger : Logger
    {
        #region Properties

        public Stack<DateTime> StartTimes { get; private set; } = new Stack<DateTime>();

        #endregion

        #region Methods

        public void WriteStart(string msg)
        {
            Write(msg);

            StartTimes.Push(DateTime.UtcNow);
        }

        public void WriteStart(string fmt,  params object[] args)
        {
            Write(fmt, args);

            StartTimes.Push(DateTime.UtcNow);
        }

        public void WriteLineStart(string msg)
        {
            WriteLine(msg);

            StartTimes.Push(DateTime.UtcNow);
        }

        public void WriteLineStart(string fmt, params object[] args)
        {
            WriteLine(fmt, args);

            StartTimes.Push(DateTime.UtcNow);
        }

        public void WriteLineEnd(string fmt, params object[] args)
        {
            var s = string.Format(fmt, args);
            var st = StartTimes.Pop();
            var et = DateTime.UtcNow;
            var elapsed = et - st;

            s = string.Format("{0} ({1} secs elapsed)", s, elapsed.TotalSeconds);
            WriteLine(s);
        }

        public void InplaceWriteLineEnd(int back, string fmt, params object[] args)
        {
            var s = string.Format(fmt, args);
            var st = StartTimes.Pop();
            var et = DateTime.UtcNow;
            var elapsed = et - st;

            s = string.Format("{0} ({1} secs elapsed)", s, elapsed.TotalSeconds);
            InplaceWriteLine(back, s);
        }

        #endregion
    }
}
