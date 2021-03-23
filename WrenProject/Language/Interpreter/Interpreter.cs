using System;
using Language.Parser;
using Language.Parser.Statement;

namespace Language
{
    public class Interpreter : IVisitor
    {
        private Block Block { get; }

        /// <summary>
        /// Enviroment for native functions of System "class"
        /// </summary>
        private static readonly Environment SystemEnvironment = new();

        /// <summary>
        /// Enviroment for native functions of Turtle "class"
        /// </summary>
        private static readonly Environment Turtle = new(SystemEnvironment);

        private Environment _environment = new(Turtle);


        /// <summary>
        /// Define native functions.
        /// </summary>
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
            Block = block;
        }

        public void Interpret()
        {
            Block.Accept(this);
        }

        public object VisitAssignStmt(AssignStatement assignStatement)
        {
            var value = assignStatement.Value.Accept(this);
            _environment.Assign(assignStatement.Name, value);
            return value;
        }

        public object VisitCallStmt(CallStatement callStatement)
        {
            var callClass = callStatement.Class;
            var identifier = callStatement.Name;

            var arguments = callStatement.Arguments;

            switch (callClass.Type)
            {
                case TokenType.System when SystemEnvironment.Get(identifier) is ICallable function:
                    return function.Call(this, arguments);
                case TokenType.Turtle when Turtle.Get(identifier) is ICallable function:
                    return function.Call(this, arguments);
                default:
                    throw new ArgumentException("Calling method from unknown class.");
            }
        }

        public object VisitIfStmt(IfStatement ifStatement)
        {
            if ((bool) ifStatement.Condition.Accept(this))
            {
                ifStatement.Then.Accept(this);
            }
            else
            {
                ifStatement.Else.Accept(this);
            }

            return null;
        }

        public object VisitWhileStmt(WhileStatement whileStatement)
        {
            while ((bool) whileStatement.Condition.Accept(this))
            {
                whileStatement.Do.Accept(this);
            }

            return null;
        }

        public object VisitBinaryExpr(BinaryExpression binary)
        {
            switch (binary.Operator.Type)
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

            throw new ArgumentException("Unknown expression operator.");
        }

        public object VisitBlock(Block block)
        {
            var oldEnvironment = _environment;
            try
            {
                _environment = new Environment(_environment);
                foreach (var statement in block.Statements)
                {
                    statement.Accept(this);
                }
            }
            finally
            {
                _environment = oldEnvironment;
            }

            return null;
        }

        public object VisitNumber(Number number)
        {
            return number.Value;
        }

        public object VisitUnaryExpression(UnaryExpression unary)
        {
            switch (unary.Operator.Type)
            {
                case TokenType.Minus:
                    return -(double) unary.Right.Accept(this);
                case TokenType.Plus:
                    return +(double) unary.Right.Accept(this);
            }

            throw new ArgumentException("Unknown symbol in unary expresion");
        }

        public object VisitVar(Var var)
        {
            _environment.Define(var.Name, var.Value.Accept(this));
            return null;
        }

        public object VisitVariable(Variable variable)
        {
            return _environment.Get(variable.Name);
        }

        public object VisitStringLiteral(StringLiteral expr)
        {
            return expr.Value;
        }
    }
}