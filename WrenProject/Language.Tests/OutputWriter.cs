using System.IO;
using System.Text;

namespace Language.Tests
{
    public class OutputWriter : TextWriter
    {
        public string output;

        public OutputWriter(string output)
        {
            this.output = output;
        }

        public override void WriteLine(char value)
        {
            output += value + "\n";
        }

        public override void WriteLine(string value)
        {
            output += value + "\n";
        }

        public override Encoding Encoding { get; }
    }
}