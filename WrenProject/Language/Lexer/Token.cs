namespace Language.Lexer
{
    public class Token
    {
        /// <summary>
        /// Type of token
        /// </summary>
        public TokenType Type { get; }

        /// <summary>
        /// Value of token
        /// </summary>
        public object Literal { get; }


        public Token(TokenType type, object literal)
        {
            Type = type;
            Literal = literal;
        }
    }
}