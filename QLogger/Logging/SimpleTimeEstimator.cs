using System;
using System.Collections.Generic;

namespace QLogger.Logging
{
    public class SimpleTimeEstimator
    {
        private class Record
        {
            public Record(DateTime time, double percentage)
            {
                Time = time;
                Percentage = percentage;
            }
            public DateTime Time { get; }
            public double Percentage { get; }
        }

        private Queue<Record> _queue = new Queue<Record>();

        public int QueueLength { get; private set; }

        public DateTime StartTime { get; private set; }
        
        /// <summary>
        ///  Last reporting time
        /// </summary>
        public DateTime LastTime { get; private set; }

        /// <summary>
        ///  Last reported percentage
        /// </summary>
        public double LastPercentage { get; private set; }

        /// <summary>
        ///  Diff between this LastTime and StartTime 
        /// </summary>
        public TimeSpan TotalElapsed => LastTime - StartTime;

        /// <summary>
        ///  Estimated remaining time 
        /// </summary>
        public TimeSpan? Estimate { get; private set; }
        
        public SimpleTimeEstimator(int queueLen = 16)
        {
            QueueLength = queueLen;
        }

        public void Start()
        {
            StartTime = DateTime.UtcNow;
            _queue.Clear();
            _queue.Enqueue(new Record(StartTime, 0));
        }

        public void Report(double percentage)  
        {
            lock (this)
            {
                var currTime = DateTime.UtcNow;
                _queue.Enqueue(new Record(currTime, percentage));
                while (_queue.Count > QueueLength)
                {
                    _queue.Dequeue();
                }
                var first = _queue.Peek();
                var timeDiff = currTime - first.Time;
                var progress = percentage - first.Percentage;
                if (timeDiff.TotalSeconds < double.Epsilon)
                {
                    Estimate = null; // instable result, can't estimate
                }
                else
                {
                    var speed = progress / timeDiff.TotalSeconds;
                    if (speed < double.Epsilon)
                    {
                        Estimate = null; // can't estimate
                    }
                    else
                    {
                        var remaining = (1 - percentage) / speed;
                        Estimate = TimeSpan.FromSeconds(remaining);
                    }
                }
                LastTime = currTime;
                LastPercentage = percentage;
            }
        }
    }
}
