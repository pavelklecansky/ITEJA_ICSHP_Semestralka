using System;
using System.Linq;
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
            var left = binary.Left.Accept(this);
            var right = binary.Right.Accept(this);
            switch (binary.Operator.Type)
            {
                case TokenType.Plus:
                    return Sum(left, right);
                case TokenType.Minus:
                    return Subtract(left, right);
                case TokenType.Slash:
                    return Divide(left, right);
                case TokenType.Star:
                    return Multiply(left, right);
                case TokenType.Equal:
                    return IsEqual(left, right);
                case TokenType.NotEqual:
                    return !IsEqual(left, right);
                case TokenType.Less:
                    return LessCompare(left, right);
                case TokenType.LessEqual:
                    return LessEqualCompare(left, right);
                case TokenType.Greater:
                    return GreaterCompare(left, right);
                case TokenType.GreaterEqual:
                    return GreaterEqualCompare(left, right);
                case TokenType.Modulo:
                    return Modulo(left, right);
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

        private object Multiply(object left, object right)
        {
            if (left is double leftDouble && right is double rightDouble)
            {
                return leftDouble * rightDouble;
            }

            if (left is string leftString && right is double rightDouble2)
            {
                return string.Concat(Enumerable.Repeat(leftString, (int) rightDouble2));
            }

            throw new ArgumentException("You must multiply numbers or string by number");
        }

        private object Divide(object left, object right)
        {
            if (left is double leftDouble && right is double rightDouble)
            {
                return leftDouble / rightDouble;
            }

            throw new ArgumentException("You must divide numbers.");
        }

        private object Subtract(object left, object right)
        {
            if (left is double leftDouble && right is double rightDouble)
            {
                return leftDouble - rightDouble;
            }

            throw new ArgumentException("You must subtract numbers.");
        }

        private object Sum(object left, object right)
        {
            if (left is string leftString && right is string rightString)
            {
                return leftString + rightString;
            }

            if (left is double leftDouble && right is double rightDouble)
            {
                return leftDouble + rightDouble;
            }

            throw new ArgumentException("You must concat same types");
        }

        private bool IsEqual(object left, object right)
        {
            if (left is string leftString && right is string rightString)
            {
                return leftString == rightString;
            }

            if (left is double leftDouble && right is double rightDouble)
            {
                return leftDouble == rightDouble;
            }

            return left == right;
        }

        private object GreaterEqualCompare(object left, object right)
        {
            if (left is double leftDouble && right is double rightDouble)
            {
                return leftDouble >= rightDouble;
            }

            throw new ArgumentException("You can't use >= on string.");
        }

        private object GreaterCompare(object left, object right)
        {
            if (left is double leftDouble && right is double rightDouble)
            {
                return leftDouble > rightDouble;
            }

            throw new ArgumentException("You can't use > on string.");
        }

        private object LessEqualCompare(object left, object right)
        {
            if (left is double leftDouble && right is double rightDouble)
            {
                return leftDouble <= rightDouble;
            }

            throw new ArgumentException("You can't use <= on string.");
        }

        private object LessCompare(object left, object right)
        {
            if (left is double leftDouble && right is double rightDouble)
            {
                return leftDouble < rightDouble;
            }

            throw new ArgumentException("You can't use < on string.");
        }

        private object Modulo(object left, object right)
        {
            if (left is double leftDouble && right is double rightDouble)
            {
                return leftDouble % rightDouble;
            }

            throw new ArgumentException("You can't use % on string.");
        }
    }
}