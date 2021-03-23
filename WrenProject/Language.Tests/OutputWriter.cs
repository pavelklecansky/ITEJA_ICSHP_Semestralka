using System.IO;
using System.Text;

namespace Language.Tests
{
    public class OutputWriter : TextWriter
    {
        public string Output;

        public OutputWriter(string output)
        {
            Output = output;
        }

        public override void WriteLine(char value)
        {
            Output += value + "\n";
        }

        public override void WriteLine(string value)
        {
            Output += value + "\n";
        }

        public override Encoding Encoding { get; }
    }
}