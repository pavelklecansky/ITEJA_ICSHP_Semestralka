using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Language.Parser.Statement;

namespace Language.Parser
{
    public class Parser
    {
        private readonly List<Token> tokens;

        private int current;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public Block Parse()
        {
            return Program();
        }

        private bool IsNext(params TokenType[] types)
        {
            foreach (var type in types)
            {
                if (Verify(type))
                {
                    GetTokenAndAdvance();
                    return true;
                }
            }

            return false;
        }

        private bool Verify(TokenType type)
        {
            if (End())
            {
                return false;
            }

            return LookAhead().Type == type;
        }

        private Token LookAhead()
        {
            return tokens[current];
        }

        private bool End()
        {
            return LookAhead().Type == TokenType.Eof;
        }

        private Token LookBack()
        {
            return tokens[current - 1];
        }

        private Token GetTokenAndAdvance()
        {
            if (!End()) current++;
            return LookBack();
        }

        private Token FindOrError(TokenType token, string message)
        {
            if (Verify(token))
            {
                return GetTokenAndAdvance();
            }

            throw new ArgumentException(message);
        }

        private Block Program()
        {
            Block block = Block();

            if (!End())
            {
                throw new ArgumentException("Unexpected expresion.");
            }

            return block;
        }

        private Block Block()
        {
            List<IStatement> statements = Statements();
            return new Block(statements);
        }

        private List<IStatement> Statements()
        {
            var statements = new List<IStatement>();
            while (!End() && tokens[current].Type != TokenType.RightBracket)
            {
                statements.Add(Statement());
            }

            return statements;
        }

        private IStatement Statement()
        {
            if (IsNext(TokenType.If))
            {
                return IfStatement();
            }

            if (IsNext(TokenType.While))
            {
                return WhileStatement();
            }

            if (IsNext(TokenType.Var))
            {
                return Var();
            }

            if (IsNext(TokenType.System, TokenType.Turtle))
            {
                return Call();
            }

            if (IsNext(TokenType.Identifier))
            {
                return AssignStatment();
            }

            return null;
        }

        private IStatement Call()
        {
            Token cl = LookBack();
            List<IExpression> arguments = new List<IExpression>();
            FindOrError(TokenType.Period, "Expect . after Class");
            if (IsNext(TokenType.Identifier))
            {
                var identValue = Indentifier();
                FindOrError(TokenType.LeftParen, "Expect '(' after 'if'.");
                if (!Verify(TokenType.RightParen))
                {
                    do
                    {
                        arguments.Add(ExpOrString());
                    } while (IsNext(TokenType.Comma));
                }

                FindOrError(TokenType.RightParen, "Expect ')' after 'if'.");
                return new CallStmt(cl, identValue, arguments);
            }


            throw new ArgumentException("Unexpected expresion.");
        }

        private IStatement AssignStatment()
        {
            var identValue = Indentifier();
            FindOrError(TokenType.Assignment, "Expect '=' after identifier.");
            IExpression expression = ExpOrString();
            return new AssignStmt(identValue, expression);
        }

        private IExpression ExpOrString()
        {
            if (IsNext(TokenType.String))
            {
                Token cl = LookBack();
                return new StringLiteral((string) cl.Literal);
            }

            return Expression();
        }

        private IStatement Var()
        {
            var name = FindOrError(TokenType.Identifier, "Expect '(' after 'if'.");
            IExpression init = null;
            if (IsNext(TokenType.Assignment))
            {
                init = ExpOrString();
            }

            return new Var(name.Literal.ToString(), init);
        }

        private IStatement WhileStatement()
        {
            FindOrError(TokenType.LeftParen, "Expect '(' after 'if'.");
            IExpression condition = Expression();
            FindOrError(TokenType.RightParen, "Expect ')' after 'if'.");
            FindOrError(TokenType.LeftBracket, "Expect '{' ");
            Block then = Block();
            FindOrError(TokenType.RightBracket, "Expect '}' ");
            return new WhileStmt(condition, then);
        }

        private IStatement IfStatement()
        {
            FindOrError(TokenType.LeftParen, "Expect '(' after 'if'.");
            IExpression condition = Expression();
            FindOrError(TokenType.RightParen, "Expect ')' after 'if'.");
            FindOrError(TokenType.LeftBracket, "Expect '{' ");
            Block then = Block();
            FindOrError(TokenType.RightBracket, "Expect '}' ");
            Block elseB = null;
            if (IsNext(TokenType.Else))
            {
                FindOrError(TokenType.LeftBracket, "Expect '{' ");
                elseB = Block();
                FindOrError(TokenType.RightBracket, "Expect '}' ");
            }

            return new IfStmt(condition, then, elseB);
        }

        private IExpression Expression()
        {
            return Equality();
        }

        private IExpression Equality()
        {
            IExpression expression = ComparisonExp();

            while (IsNext(TokenType.Equal, TokenType.NotEqual))
            {
                Token oper = LookBack();
                IExpression right = ComparisonExp();
                expression = new BinaryExpression(expression, oper, right);
            }

            return expression;
        }

        private IExpression ComparisonExp()
        {
            IExpression expression = Term();
            while (IsNext(TokenType.Less, TokenType.LessEqual, TokenType.Greater, TokenType.GreaterEqual))
            {
                Token oper = LookBack();
                IExpression right = Expression();
                expression = new BinaryExpression(expression, oper, right);
            }

            return expression;
        }

        private IExpression Term()
        {
            IExpression expr = Factor();
            while (IsNext(TokenType.Plus, TokenType.Minus))
            {
                Token oper = LookBack();
                IExpression right = Factor();
                expr = new BinaryExpression(expr, oper, right);
            }

            return expr;
        }

        private IExpression Factor()
        {
            IExpression expr = Unary();
            while (IsNext(TokenType.Star, TokenType.Slash, TokenType.Modulo))
            {
                Token oper = LookBack();
                IExpression right = Unary();
                expr = new BinaryExpression(expr, oper, right);
            }

            return expr;
        }

        private IExpression Unary()
        {
            if (IsNext(TokenType.Plus, TokenType.Minus))
            {
                Token oper = LookBack();
                IExpression right = Unary();
                return new UnaryExpression(oper, right);
            }

            return Primary();
        }

        private IExpression Primary()
        {
            if (IsNext(TokenType.Number))
            {
                return new Number((double) LookBack().Literal);
            }

            if (IsNext(TokenType.Identifier))
            {
                return new Variable(Indentifier());
            }

            if (IsNext(TokenType.LeftParen))
            {
                IExpression expr = Expression();
                FindOrError(TokenType.RightParen, "Right paren wasn't find");
                return expr;
            }

            throw new ArgumentException("Unexpected expresion.");
        }

        private string Indentifier()
        {
            Token indent = LookBack();
            string identValue;
            if (indent.Type == TokenType.Identifier)
            {
                identValue = (string) indent.Literal;
            }
            else
            {
                throw new ArgumentException("Unexpected expresion.");
            }

            return identValue;
        }
    }
}