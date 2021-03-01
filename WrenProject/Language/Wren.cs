using System;
using System.Collections.Generic;

namespace Language
{
    internal class Wren
    {
        private static bool _hadError = false;

        static void Main(string[] args)
        {
            var source = $"System.print(\"Hi\")";
            Lexer lexer = new Lexer(source);

            List<Token> tokens = lexer.GetTokens();

            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }

        public static void Error(int line, string message)
        {
            Console.Error.WriteLine($"Line: {line}, Error: {message}");
        }
    }
}