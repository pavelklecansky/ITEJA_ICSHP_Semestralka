namespace Language
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