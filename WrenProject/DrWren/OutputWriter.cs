using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DrWren
{
    public class OutputWriter : TextWriter
    {
        private readonly RichTextBox _output;

        public OutputWriter(RichTextBox output)
        {
            _output = output;
            Encoding = Encoding.Default;
        }

        public override void WriteLine(char value)
        {
            _output.Text += value + NewLine;
        }

        public override void WriteLine(string value)
        {
            _output.Text += value + NewLine;
        }

        public override Encoding Encoding { get; }
    }
}