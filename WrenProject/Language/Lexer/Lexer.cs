using System;
using System.Collections.Generic;

namespace Language.Lexer
{
    internal class Lexer
    {
        private readonly List<Token> Tokens = new();
        private static readonly Dictionary<string, TokenType> Keywords = new();

        private readonly string _source;
        private int _start = 0;
        private int _current = 0;
        private int _line = 1;

        static Lexer()
        {
            Keywords.Add("var", TokenType.Var);
            Keywords.Add("while", TokenType.While);
            Keywords.Add("if", TokenType.If);
            Keywords.Add("else", TokenType.Else);
            Keywords.Add("System", TokenType.System);
            Keywords.Add("Turtle", TokenType.Turtle);
        }

        public Lexer(string source)
        {
            _source = source;
        }

        public List<Token> GetTokens()
        {
            while (!End())
            {
                _start = _current;
                GetToken();
            }

            Tokens.Add(new Token(TokenType.Eof, null, _line));
            return Tokens;
        }

        private void GetToken()
        {
            char c = GetCharAndAdvance();
            switch (c)
            {
                case '(':
                    AddToken(TokenType.LeftParen);
                    break;
                case ')':
                    AddToken(TokenType.RightParen);
                    break;
                case '{':
                    AddToken(TokenType.LeftBracket);
                    break;
                case '}':
                    AddToken(TokenType.RightBracket);
                    break;
                case ';':
                    AddToken(TokenType.Semicolon);
                    break;
                case '+':
                    AddToken(TokenType.Plus);
                    break;
                case '-':
                    AddToken(TokenType.Minus);
                    break;
                case '/':
                    AddToken(TokenType.Slash);
                    break;
                case '%':
                    AddToken(TokenType.Modulo);
                    break;
                case '*':
                    AddToken(TokenType.Star);
                    break;
                case ',':
                    AddToken(TokenType.Comma);
                    break;
                case '.':
                    AddToken(TokenType.Period);
                    break;
                case '!':
                    AddToken(IsNext('=') ? TokenType.NotEqual : throw new ArgumentException("Bad syntax"));
                    break;
                case '=':
                    AddToken(IsNext('=') ? TokenType.Equal : TokenType.Assignment);
                    break;
                case '<':
                    AddToken(IsNext('=') ? TokenType.LessEqual : TokenType.Less);
                    break;
                case '>':
                    AddToken(IsNext('=') ? TokenType.GreaterEqual : TokenType.Greater);
                    break;
                case ' ':
                case '\r':
                case '\t':
                    break;
                case '\n':
                    _line++;
                    break;
                case '"':
                    ReadString();
                    break;
                default:
                    if (char.IsNumber(c))
                    {
                        ReadNumber();
                    }
                    else if (char.IsLetter(c))
                    {
                        Identifier();
                    }
                    else
                    {
                        Wren.Error(_line, "Unexpected character.");
                    }

                    break;
            }
        }

        private void ReadString()
        {
            while (LookAhead() != '"' && !End())
            {
                if (LookAhead() == '\n')
                {
                    _line++;
                }

                GetCharAndAdvance();
            }

            if (End())
            {
                Wren.Error(_line, "Unterminated string.");
                return;
            }

            GetCharAndAdvance();

            string value = _source.Substring(_start + 1, _current - _start - 2);
            AddToken(TokenType.String, value);
        }

        private void Identifier()
        {
            while (char.IsLetterOrDigit(char.ToLower(LookAhead())))
            {
                GetCharAndAdvance();
            }

            string text = _source.Substring(_start, _current - _start);
            if (Keywords.TryGetValue(text, out var type))
            {
                AddToken(type);
            }
            else
            {
                AddToken(TokenType.Identifier, text);
            }
        }

        private void ReadNumber()
        {
            while (char.IsDigit(LookAhead()))
            {
                GetCharAndAdvance();
            }

            if (LookAhead() == '.' && char.IsDigit(LookAheadNext()))
            {
                GetCharAndAdvance();
                while (char.IsDigit(LookAhead())) GetCharAndAdvance();
            }

            AddToken(TokenType.Number,
                double.Parse(_source.Substring(_start, _current - _start),
                    System.Globalization.CultureInfo.InvariantCulture));
        }

        private char LookAhead()
        {
            if (End())
            {
                return '\0';
            }

            return _source[_current];
        }

        private char LookAheadNext()
        {
            if (_current + 1 >= _source.Length) return '\0';
            return _source[_current + 1];
        }

        private bool IsNext(char expected)
        {
            if (End()) return false;
            if (_source[_current] != expected)
            {
                return false;
            }

            _current++;
            return true;
        }

        private void AddToken(TokenType type)
        {
            AddToken(type, null);
        }

        private void AddToken(TokenType type, object? literal)
        {
            Tokens.Add(new Token(type, literal, _line));
        }

        private char GetCharAndAdvance()
        {
            return _source[_current++];
        }

        private bool End()
        {
            return _current >= _source.Length;
        }
    }
}