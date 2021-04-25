using System;
using System.IO;
using System.Windows.Forms;

namespace DrWren
{
    public class CodeEventArgs : EventArgs
    {
        public long CodeRanTime { get; set; }
    }

    public class CodeRunner
    {
        private readonly string _sourceCode;
        private readonly TextWriter _writer;

        public delegate void CodeRanEventHandler(object source, CodeEventArgs args);

        public event CodeRanEventHandler CodeRan;

        private long CodeRanTime { get; set; }

        public CodeRunner(string sourceCode, TextWriter writer)
        {
            _sourceCode = sourceCode;
            _writer = writer;
        }

        public void Run()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                Language.Wren.Run(_sourceCode, _writer);
                watch.Stop();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, e.HelpLink,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                watch.Stop();
            }

            CodeRanTime = watch.ElapsedMilliseconds;
            OnCodeRan();
        }

        protected virtual void OnCodeRan()
        {
            if (CodeRan != null)
            {
                CodeRan(this, new CodeEventArgs {CodeRanTime = CodeRanTime});
            }
        }
    }
}