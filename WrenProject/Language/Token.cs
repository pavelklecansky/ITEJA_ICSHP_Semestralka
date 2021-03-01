using System;

namespace Language
{
    public class Token
    {
        private TokenType Type { get; }
        private object? Literal { get; }

        private int Line { get; }
        

        public Token(TokenType type, object? literal, int line)
        {
            Type = type;
            Literal = literal;
            Line = line;
        }

        public override string ToString()
        {
            var token = $"Token {Type}";
            if (Literal != null)
            {
                token += $", value \"{Literal}\"";
            }

            return token;
        }
    }
}