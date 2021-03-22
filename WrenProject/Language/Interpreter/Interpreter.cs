using System;
using Language.Parser;
using Language.Parser.Statement;

namespace Language
{
    public class Interpreter : IVisiter
    {
        private Block block;
        private static readonly Environment SystemEnvironment = new();
        private static readonly Environment Turtle = new(SystemEnvironment);
        private Environment environment = new(Turtle);

        static Interpreter()
        {
            SystemEnvironment.Define("print", new SystemClass.Print());
            Turtle.Define("left", new TurtleClass.Left());
            Turtle.Define("done", new TurtleClass.Done());
            Turtle.Define("forward", new TurtleClass.Forward());
            Turtle.Define("right", new TurtleClass.Right());
            Turtle.Define("rotate", new TurtleClass.Right());
        }

        public Interpreter(Block block)
        {
            this.block = block;
        }

        public void Interpret()
        {
            block.Accept(this);
        }

        public object VisitAssignStmt(AssignStmt assignStmt)
        {
            Object value = assignStmt.Value.Accept(this);
            environment.Assign(assignStmt.Name, value);
            return value;
        }

        public object VisitCallStmt(CallStmt callStmt)
        {
            var callClass = callStmt.Class;
            var identifier = callStmt.Name;

            var arguments = callStmt.Arguments;

            if (callClass.Type == TokenType.System)
            {
                if (SystemEnvironment.Get(identifier) is ICallable function)
                {
                    return function.Call(this, arguments);
                }
            }

            if (callClass.Type == TokenType.Turtle)
            {
                if (Turtle.Get(identifier) is ICallable function)
                {
                    return function.Call(this, arguments);
                }
            }

            throw new ArgumentException("Unexpected expresion.");
        }

        public object VisitIfStmt(IfStmt ifStmt)
        {
            if ((bool) ifStmt.Condition.Accept(this))
            {
                ifStmt.Then.Accept(this);
            }
            else
            {
                ifStmt.Else.Accept(this);
            }

            return null;
        }

        public object VisitWhileStmt(WhileStmt whileStmt)
        {
            while ((bool) whileStmt.Condition.Accept(this))
            {
                whileStmt.Do.Accept(this);
            }

            return null;
        }

        public object VisitBinaryExpr(BinaryExpression binary)
        {
            switch (binary.Oper.Type)
            {
                case TokenType.Plus:
                    return (double) binary.Left.Accept(this) + (double) binary.Right.Accept(this);
                case TokenType.Minus:
                    return (double) binary.Left.Accept(this) - (double) binary.Right.Accept(this);
                case TokenType.Slash:
                    return (double) binary.Left.Accept(this) / (double) binary.Right.Accept(this);
                case TokenType.Star:
                    return (double) binary.Left.Accept(this) * (double) binary.Right.Accept(this);
                case TokenType.Equal:
                    return (double) binary.Left.Accept(this) == (double) binary.Right.Accept(this);
                case TokenType.NotEqual:
                    return (double) binary.Left.Accept(this) != (double) binary.Right.Accept(this);
                case TokenType.Less:
                    return (double) binary.Left.Accept(this) < (double) binary.Right.Accept(this);
                case TokenType.LessEqual:
                    return (double) binary.Left.Accept(this) <= (double) binary.Right.Accept(this);
                case TokenType.Greater:
                    return (double) binary.Left.Accept(this) > (double) binary.Right.Accept(this);
                case TokenType.GreaterEqual:
                    return (double) binary.Left.Accept(this) >= (double) binary.Right.Accept(this);
                case TokenType.Modulo:
                    return (double) binary.Left.Accept(this) % (double) binary.Right.Accept(this);
            }

            throw new ArgumentException("Unexpected expresion.");
        }

        public object VisitBlock(Block block)
        {
            var oldEnvironment = environment;
            try
            {
                environment = new Environment(environment);
                foreach (var statement in block.Statements)
                {
                    statement.Accept(this);
                }
            }
            finally
            {
                environment = oldEnvironment;
            }

            return null;
        }

        public object VisitNumber(Number number)
        {
            return number.Value;
        }

        public object VisitUnaryExpression(UnaryExpression unary)
        {
            switch (unary.Oper.Type)
            {
                case TokenType.Plus:
                    return +(double) unary.Right.Accept(this);
                case TokenType.Minus:
                    return -(double) unary.Right.Accept(this);
            }

            throw new ArgumentException("Unknown symbol in unary expresion");
        }

        public object VisitVar(Var var)
        {
            environment.Define(var.Name, var.Value.Accept(this));
            return null;
        }

        public object VisitVariable(Variable variable)
        {
            return environment.Get(variable.Name);
        }

        public object VisitStringLiteral(StringLiteral expr)
        {
            return expr.Value;
        }
    }
}