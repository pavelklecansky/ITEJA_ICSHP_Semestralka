namespace Language
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
        
        //Jedno nebo dvou charakterové tokeny
        Greater,
        GreaterEqual,
        Less,
        LessEqual,
        Assignment,
        Equal,


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

        Eof
    }
}