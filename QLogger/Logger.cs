using System;
using System.Collections.Generic;
using System.IO;
using QLogger.Helpers;

namespace QLogger
{
    public class Logger
    {
        #region Properties

        public ISet<TextWriter> Writers { get; private set; } = new HashSet<TextWriter>();

        #endregion

        #region Methods

        public void Write(string msg)
        {
            InplaceWrite(0, msg);
        }

        public void Write(string fmt, params object[] args)
        {
            InplaceWrite(0, fmt, args);
        }

        public void WriteLine(string msg)
        {
            InplaceWriteLine(0, msg);
        }

        public void WriteLine(string fmt, params object[] args)
        {
            InplaceWriteLine(0, fmt, args);
        }

        public int InplaceWrite(int back, string msg)
        {
            var res = 0;
            foreach (var writer in Writers)
            {
                if (writer == Console.Out)
                {
                    res = back.InplaceWrite(msg);
                }
                else
                {
                    writer.Write(msg);
                }
            }
            return res;
        }

        public int InplaceWrite(int back, string fmt, params object[] args)
        {
            var res = 0;
            foreach (var writer in Writers)
            {
                if (writer == Console.Out)
                {
                    res = back.InplaceWrite(fmt, args);
                }
                else
                {
                    writer.Write(fmt, args);
                }
            }
            return res;
        }

        public void InplaceWriteLine(int back, string msg)
        {
            foreach (var writer in Writers)
            {
                if (writer == Console.Out)
                {
                    back.InplaceWriteLine(msg);
                }
                else
                {
                    writer.WriteLine(msg);
                }
            }
        }

        public void InplaceWriteLine(int back, string fmt, params object[] args)
        {
            foreach (var writer in Writers)
            {
                if (writer == Console.Out)
                {
                    back.InplaceWriteLine(fmt, args);
                }
                else
                {
                    writer.WriteLine(fmt, args);
                }
            }
        }

        #endregion
    }
}
