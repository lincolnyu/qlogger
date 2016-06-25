using System;
using static QLogger.Logging.Stopwatch;

namespace QLogger.Logging
{
    public class StopwatchItem : IDisposable
    {
        private EndCallback _cb;
        private Stopwatch _stopWatch;

        public StopwatchItem(Stopwatch watch, EndCallback cb)
        {
            _cb = cb;
            _stopWatch = watch;
        }

        public void Dispose()
        {
            _stopWatch.End(_cb);
        }
    }
}
