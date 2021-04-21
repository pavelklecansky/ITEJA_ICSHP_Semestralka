using System;
using System.Linq;
using Language.Interpreter.NativeLibrary;
using Language.Lexer;
using Language.Parser;
using Language.Parser.Statement;

namespace Language.Interpreter
{
    /// <summary>
    /// Wren interpreter.
    /// </summary>
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

        /// <summary>
        /// Interprets and executes block given in constructor.
        /// </summary>
        public void Interpret()
        {
            Block.Accept(this);
        }

        /// <summary>
        /// Run assign statement
        /// </summary>
        /// <param name="assignStatement">Statement to run</param>
        /// <returns>Assign value</returns>
        public object VisitAssignStmt(AssignStatement assignStatement)
        {
            var value = assignStatement.Value.Accept(this);
            _environment.Assign(assignStatement.Name, value);
            return value;
        }

        /// <summary>
        /// Run call statement
        /// </summary>
        /// <param name="callStatement">Statement to run</param>
        /// <returns>Call value</returns>
        /// <exception cref="ArgumentException"></exception>
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

        /// <summary>
        /// Run if statement
        /// </summary>
        /// <param name="ifStatement">Statement to run</param>
        /// <returns>Null</returns>
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

        /// <summary>
        /// Run while statement
        /// </summary>
        /// <param name="whileStatement">Statement to run</param>
        /// <returns>Nulls</returns>
        public object VisitWhileStmt(WhileStatement whileStatement)
        {
            while ((bool) whileStatement.Condition.Accept(this))
            {
                whileStatement.Do.Accept(this);
            }

            return null;
        }

        /// <summary>
        /// Evaluates a binary expression
        /// </summary>
        /// <param name="binary">Expression</param>
        /// <returns>Result of the expression</returns>
        /// <exception cref="ArgumentException">Unknown binary expression operator.</exception>
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

            throw new ArgumentException("Unknown binary expression operator.");
        }

        /// <summary>
        /// Run block
        /// </summary>
        /// <param name="block">Block to run</param>
        /// <returns>Null</returns>
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

        /// <summary>
        /// Evalutes a number exression
        /// </summary>
        /// <param name="number">Number expression</param>
        /// <returns>Value of number</returns>
        public object VisitNumber(Number number)
        {
            return number.Value;
        }

        /// <summary>
        /// Evaluates a unary expression
        /// </summary>
        /// <param name="unary">Expression</param>
        /// <returns>Result of the expression</returns>
        /// <exception cref="ArgumentException">Unknown symbol in unary expresion</exception>
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

        /// <summary>
        /// Creates new variable
        /// </summary>
        /// <param name="var">Variable</param>
        /// <returns>Null</returns>
        public object VisitVar(Var var)
        {
            _environment.Define(var.Name, var.Value.Accept(this));
            return null;
        }

        /// <summary>
        /// Get existing variable
        /// </summary>
        /// <param name="variable">Variable</param>
        /// <returns>Variable value</returns>
        public object VisitVariable(Variable variable)
        {
            return _environment.Get(variable.Name);
        }

        /// <summary>
        /// Evalutes a string exression
        /// </summary>
        /// <param name="expr">String expression</param>
        /// <returns>Value of string</returns>
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