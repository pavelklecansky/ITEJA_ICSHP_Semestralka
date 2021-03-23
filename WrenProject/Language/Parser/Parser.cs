using System;
using System.Collections.Generic;
using System.Linq;
using Language.Parser.Statement;

namespace Language.Parser
{
    public class Parser
    {
        private readonly List<Token> _tokens;

        private int _current;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
        }

        public Block Parse()
        {
            return Program();
        }

        private bool IsNext(params TokenType[] types)
        {
            if (types.Any(Verify))
            {
                GetTokenAndAdvance();
                return true;
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
            return _tokens[_current];
        }

        private bool End()
        {
            return LookAhead().Type == TokenType.Eof;
        }

        private Token LookBack()
        {
            return _tokens[_current - 1];
        }

        private Token GetTokenAndAdvance()
        {
            if (!End()) _current++;
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
            var block = Block();

            if (!End())
            {
                throw new ArgumentException("Unexpected expresion.");
            }

            return block;
        }

        private Block Block()
        {
            var statements = Statements();
            return new Block(statements);
        }

        private List<IStatement> Statements()
        {
            var statements = new List<IStatement>();
            while (!End() && _tokens[_current].Type != TokenType.RightBracket)
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
            var className = LookBack();
            var arguments = new List<IExpression>();
            FindOrError(TokenType.Period, "Expect . after Class");
            if (IsNext(TokenType.Identifier))
            {
                var identifierValue = Indentifier();
                FindOrError(TokenType.LeftParen, "Expect '(' after 'if'.");
                if (!Verify(TokenType.RightParen))
                {
                    do
                    {
                        arguments.Add(ExpOrString());
                    } while (IsNext(TokenType.Comma));
                }

                FindOrError(TokenType.RightParen, "Expect ')' after 'if'.");
                return new CallStmt(className, identifierValue, arguments);
            }


            throw new ArgumentException("Unexpected expresion.");
        }

        private IStatement AssignStatment()
        {
            var identifierName = Indentifier();
            FindOrError(TokenType.Assignment, "Expect '=' after identifier.");
            var expression = ExpOrString();
            return new AssignStmt(identifierName, expression);
        }

        private IExpression ExpOrString()
        {
            if (IsNext(TokenType.String))
            {
                var stringLiteral = LookBack();
                return new StringLiteral((string) stringLiteral.Literal);
            }

            return Expression();
        }

        private IStatement Var()
        {
            var name = FindOrError(TokenType.Identifier, "Expect '(' after 'if'.");
            IExpression initValue = null;
            if (IsNext(TokenType.Assignment))
            {
                initValue = ExpOrString();
            }

            return new Var(name.Literal.ToString(), initValue);
        }

        private IStatement WhileStatement()
        {
            FindOrError(TokenType.LeftParen, "Expect '(' after 'if'.");
            var condition = Expression();
            FindOrError(TokenType.RightParen, "Expect ')' after 'if'.");
            FindOrError(TokenType.LeftBracket, "Expect '{' ");
            var doBlock = Block();
            FindOrError(TokenType.RightBracket, "Expect '}' ");
            return new WhileStmt(condition, doBlock);
        }

        private IStatement IfStatement()
        {
            FindOrError(TokenType.LeftParen, "Expect '(' after 'if'.");
            var condition = Expression();
            FindOrError(TokenType.RightParen, "Expect ')' after 'if'.");
            FindOrError(TokenType.LeftBracket, "Expect '{' ");
            var then = Block();
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
            var expression = ComparisonExp();

            while (IsNext(TokenType.Equal, TokenType.NotEqual))
            {
                var expressionOperator = LookBack();
                var right = ComparisonExp();
                expression = new BinaryExpression(expression, expressionOperator, right);
            }

            return expression;
        }

        private IExpression ComparisonExp()
        {
            var expression = Term();
            while (IsNext(TokenType.Less, TokenType.LessEqual, TokenType.Greater, TokenType.GreaterEqual))
            {
                var expressionOperator = LookBack();
                var right = Expression();
                expression = new BinaryExpression(expression, expressionOperator, right);
            }

            return expression;
        }

        private IExpression Term()
        {
            var expression = Factor();
            while (IsNext(TokenType.Plus, TokenType.Minus))
            {
                var expressionOperator = LookBack();
                var right = Factor();
                expression = new BinaryExpression(expression, expressionOperator, right);
            }

            return expression;
        }

        private IExpression Factor()
        {
            var expression = Unary();
            while (IsNext(TokenType.Star, TokenType.Slash, TokenType.Modulo))
            {
                var expressionOperator = LookBack();
                var right = Unary();
                expression = new BinaryExpression(expression, expressionOperator, right);
            }

            return expression;
        }

        private IExpression Unary()
        {
            if (IsNext(TokenType.Plus, TokenType.Minus))
            {
                var expressionOperator = LookBack();
                var right = Unary();
                return new UnaryExpression(expressionOperator, right);
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
                var expr = Expression();
                FindOrError(TokenType.RightParen, "Right paren wasn't find");
                return expr;
            }

            throw new ArgumentException("Unexpected expresion.");
        }

        private string Indentifier()
        {
            var identifier = LookBack();
            string identifierValue;
            if (identifier.Type == TokenType.Identifier)
            {
                identifierValue = (string) identifier.Literal;
            }
            else
            {
                throw new ArgumentException("Unexpected expresion.");
            }

            return identifierValue;
        }
    }
}