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

        public const int DefaultSamplePeriodSeconds = 5;
        public const int DefaultRetainPeriodSeconds = 60;

        public SimpleTimeEstimator(int samplePeriodSeconds = DefaultSamplePeriodSeconds, int retainPeriodSeconds = DefaultRetainPeriodSeconds)
            : this(TimeSpan.FromSeconds(samplePeriodSeconds), TimeSpan.FromSeconds(retainPeriodSeconds))
        {
        }

        public SimpleTimeEstimator(TimeSpan samplePeriod, TimeSpan retainPeriod)
        {
            SamplePeriod = samplePeriod;
            RetainPeriod = retainPeriod;
        }

        private Queue<Record> _queue = new Queue<Record>();
        
        /// <summary>
        ///  How often record is added to the queue roughly
        /// </summary>
        public TimeSpan SamplePeriod { get; }

        /// <summary>
        ///  How old record is retained in the queue
        /// </summary>
        public TimeSpan RetainPeriod { get; }

        /// <summary>
        ///  When Start() is called
        /// </summary>
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
                var sinceLast = currTime - LastTime;
                if (sinceLast >= SamplePeriod)
                {
                    _queue.Enqueue(new Record(currTime, percentage));
                }

                Record head;
                while (_queue.Count > 2)
                {
                    head = _queue.Peek();
                    var queuePeriod = currTime - head.Time;
                    if (queuePeriod > RetainPeriod)
                    {
                        _queue.Dequeue();
                    }
                    else
                    {
                        break;
                    }
                }
                head = _queue.Peek();
                var timeDiff = currTime - head.Time;
                var progress = percentage - head.Percentage;
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
