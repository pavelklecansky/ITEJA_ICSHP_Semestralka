using System;
using System.Collections.Generic;

namespace Language
{
    internal class Wren
    {
        private static bool _hadError = false;

        static void Main(string[] args)
        {
            var source = @"var numberOfSides = 6
var sideLenght = 70
var angle = 360 / numberOfSides
var x = 0
 
while(x < sideLenght){
    Turtle.forward(numberOfSides)
    Turtle.right(angle)
    x = x + 1
}    
Turtle.done()";
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