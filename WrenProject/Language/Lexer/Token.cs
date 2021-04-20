namespace Language.Lexer
{
    public class Token
    {
        public TokenType Type { get; }
        public object Literal { get; }


        public Token(TokenType type, object literal)
        {
            Type = type;
            Literal = literal;
        }
    }
}