using System.Collections.Generic;
using QLogger.ConsoleHelpers;
using System.IO;
using static QLogger.ConsoleHelpers.WriteMethods;
using System.Linq;

namespace QLogger.Logging
{
    public class Logger
    {
        #region Nested classes

        public class WriterWrapper
        {
            public WriterWrapper(object writer)
            {
                Writer = writer;
            }
            public object Writer { get; }
            public virtual bool FlushEveryWrite { get; set; }
            public virtual bool IsActive { get; set; }
        }

        #endregion

        #region Properties

        public ISet<WriterWrapper> Writers { get; private set; } = new HashSet<WriterWrapper>();

        #endregion

        #region Methods

        private void Write(TextWriteMethod textWrite, InplaceWriteMethod inplaceWrite, string msg)
        {
            foreach (var writer in Writers.Where(x=>x.IsActive))
            {
                var tw = writer.Writer as TextWriter;
                if (tw != null)
                {
                    textWrite(tw, msg);
                    if (writer.FlushEveryWrite) tw.Flush();
                    continue;
                }
                var iw = writer.Writer as InplaceWriter;
                if (iw != null)
                {
                    inplaceWrite(iw, msg);
                }
            }
        }

        private void WriteFormat(TextWriteFormatMethod textWrite, InplaceWriteFormatMethod inplaceWrite, string fmt, params object[] args)
        {
            foreach (var writer in Writers.Where(x => x.IsActive).Select(x => x.Writer))
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
