namespace Language.Lexer
{
    public enum TokenType
    {
        // Jedno znakové tokeny
        Semicolon,
        Minus,
        Plus,
        Slash,
        Star,
        Comma,
        LeftParen,
        RightParen,
        Period,
        LeftBracket,
        RightBracket,
        Modulo,
        
        //Jedno nebo dvou charakterové tokeny
        Greater,
        GreaterEqual,
        Less,
        LessEqual,
        Assignment,
        Equal,
        NotEqual,


        // Litrály
        Identifier,
        Number,
        String,

        // Klíčová slova
        Var,
        While,
        If,
        Turtle,
        System,
        Else,

        Eof
    }
}