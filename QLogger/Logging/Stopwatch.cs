using System;
using System.Collections.Generic;

namespace QLogger.Logging
{
    public class Stopwatch
    {
        #region Delegates

        public delegate void StartCallback(Stopwatch watch, DateTime startUtc);

        public delegate void EndCallback(Stopwatch watch,
            DateTime startUtc, DateTime endUtc);

        #endregion

        #region Properties

        public Stack<DateTime> StartTimes { get; private set; } = new Stack<DateTime>();

        #endregion

        #region Methods

        public StopwatchItem Create(EndCallback cbEnd)
        {
            Start();
            var item = new StopwatchItem(this, cbEnd);
            return item;
        }

        public StopwatchItem Create(StartCallback cbStart, EndCallback cbEnd)
        {
            Start(cbStart);
            var item = new StopwatchItem(this, cbEnd);
            return item;
        }

        public void Start(StartCallback cb = null)
        {
            var time = DateTime.UtcNow;
            StartTimes.Push(time);
            cb?.Invoke(this, time);
        }

        public void Lap(EndCallback cb = null)
        {
            var st = StartTimes.Peek();
            var et = DateTime.UtcNow;
            cb?.Invoke(this, st, et);
        }
        
        public void End(EndCallback cb = null)
        {
            var st = StartTimes.Pop();
            var et = DateTime.UtcNow;
            cb?.Invoke(this, st, et);
        }

        #endregion
    }
}
