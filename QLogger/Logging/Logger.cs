using System.Collections.Generic;
using QLogger.ConsoleHelpers;
using System.IO;
using static QLogger.ConsoleHelpers.WriteMethods;

namespace QLogger.Logging
{
    public class Logger
    {
        #region Delegates

        delegate void WriteMethod(TextWriter tw);

        #endregion

        #region Properties

        public ISet<object> Writers { get; private set; } = new HashSet<object>();

        #endregion

        #region Methods

        private void Write(TextWriteMethod textWrite, InplaceWriteMethod inplaceWrite, string msg)
        {
            foreach (var writer in Writers)
            {
                var tw = writer as TextWriter;
                if (tw != null)
                {
                    textWrite(tw, msg);
                    continue;
                }
                var iw = writer as InplaceWriter;
                if (iw != null)
                {
                    inplaceWrite(iw, msg);
                }
            }
        }

        private void WriteFormat(TextWriteFormatMethod textWrite, InplaceWriteFormatMethod inplaceWrite, string fmt, params object[] args)
        {
            foreach (var writer in Writers)
            {
                var tw = writer as TextWriter;
                if (tw != null)
                {
                    textWrite(tw, fmt, args);
                    continue;
                }
                var iw = writer as InplaceWriter;
                if (iw != null)
                {
                    inplaceWrite(iw, fmt, args);
                }
            }
        }

        public void Write(string msg)
        {
            Write(TextWrite, InplaceWrite, msg);
        }

        public void Write(string fmt, params object[] args)
        {
            WriteFormat(TextWrite, InplaceWrite, fmt, args);
        }

        public void WriteLine(string msg)
        {
            Write(TextWriteLine, InplaceWriteLine, msg);
        }

        public void WriteLine(string fmt, params object[] args)
        {
            WriteFormat(TextWriteLine, InplaceWriteLine, fmt, args);
        }

        #endregion
    }
}
