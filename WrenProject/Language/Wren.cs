using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Language
{
    public class Wren
    {
        static void Main(string[] args)
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
            string text = File.ReadAllText(s);
            Run(text);
        }

        public static void Run(string source)
        {
            Run(source,Console.Out);
        }
        
        public static void Run(string source, TextWriter output)
        {
            Console.SetOut(output);
            var lexer = new Lexer.Lexer(source);

            List<Token> tokens = lexer.GetTokens();
        
            var statements = new Parser.Parser(tokens).Parse();
        
            var inter = new Interpreter(statements);
            inter.Interpret();
        }

        internal static void Error(int line, string message)
        {
            Console.Error.WriteLine($"Line: {line}, Error: {message}");
        }
    }
}