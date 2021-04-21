using System.Collections.Generic;
using Language.Parser;

namespace Language.Interpreter.NativeLibrary
{
    /// <summary>
    /// Implementation of native Turtle class.
    /// </summary>
    internal abstract class TurtleClass
    {
        /// <summary>
        /// Rotate turtle to left
        /// </summary>
        internal class Left : ICallable
        {
            public object Call(Interpreter interpreter, List<IExpression> arguments)
            {
                Turtle.Left((double) arguments[0].Accept(interpreter));
                return null;
            }
        }

        /// <summary>
        /// Show turtle graphics
        /// </summary>
        internal class Done : ICallable
        {
            public object Call(Interpreter interpreter, List<IExpression> arguments)
            {
                Turtle.Done();
                return null;
            }
        }

        /// <summary>
        /// Move turtle forwards
        /// </summary>
        internal class Forward : ICallable
        {
            public object Call(Interpreter interpreter, List<IExpression> arguments)
            {
                switch (arguments.Count)
                {
                    case 0:
                        Turtle.Forward();
                        break;
                    case 1:
                        Turtle.Forward((double) arguments[0].Accept(interpreter));
                        break;
                }

                return null;
            }
        }

        /// <summary>
        /// Rotate turtle to right
        /// </summary>
        internal class Right : ICallable
        {
            public object Call(Interpreter interpreter, List<IExpression> arguments)
            {
                Turtle.Right((double) arguments[0].Accept(interpreter));
                return null;
            }
        }
    }
}