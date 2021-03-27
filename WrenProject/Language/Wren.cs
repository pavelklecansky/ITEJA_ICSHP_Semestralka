using System;
using System.IO;
using System.Windows.Forms;

namespace Language
{
    /// <summary>
    /// Main class for Wren interpreter.
    /// </summary>
    public static class Wren
    {
        private static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                RunFile(args[0]);
            }
            else
            {
                Console.WriteLine("Usage: wren <script>");
                Application.Exit();
            }
        }

        private static void RunFile(string s)
        {
            var text = File.ReadAllText(s);
            Run(text);
        }

        public static void Run(string source)
        {
            Run(source, Console.Out);
        }

        public static void Run(string source, TextWriter output)
        {
            Console.SetOut(output);
            var lexer = new Lexer.Lexer(source);

            var parser = new Parser.Parser(lexer.GetTokens());

            var interpreter = new Interpreter(parser.Parse());
            interpreter.Interpret();
        }
    }
}