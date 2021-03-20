using System;
using System.Collections.Generic;
using Language.Parser;

namespace Language
{
    internal class Wren
    {
        private static bool _hadError = false;

        static void Main(string[] args)
        {
            var source = @"
Turtle.left(90)
Turtle.forward(100)
Turtle.right(150)
Turtle.forward(70)
Turtle.left(120)
Turtle.forward(70)
Turtle.right(150)
Turtle.forward(100)
                            Turtle.done()";
            var lexer = new Lexer.Lexer(source);


            List<Token> tokens = lexer.GetTokens();

            var statements = new Parser.Parser(tokens).Parse();

            var inter = new Interpreter(statements);
            inter.Interpret();
            
        }

        public static void Error(int line, string message)
        {
            Console.Error.WriteLine($"Line: {line}, Error: {message}");
        }
    }
}