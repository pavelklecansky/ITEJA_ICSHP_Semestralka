using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DrWren
{
    public class OutputWriter : TextWriter
    {
        private RichTextBox output;

        public OutputWriter(RichTextBox output)
        {
            this.output = output;
        }

        public override void WriteLine(char value)
        {
            output.Text += value + "\n";
        }

        public override void WriteLine(string value)
        {
            output.Text += value + "\n";
        }

        public override Encoding Encoding { get; }
    }
}