using System.Collections.Generic;
using Language.Parser;

namespace Language.Interpreter.NativeLibrary
{
    /// <summary>
    /// Implementation of native Turtle class.
    /// </summary>
    internal abstract class TurtleClass
    {
        internal class Left : ICallable
        {
            public object Call(Interpreter interpreter, List<IExpression> arguments)
            {
                Turtle.Left((double) arguments[0].Accept(interpreter));
                return null;
            }
        }

        internal class Done : ICallable
        {
            public object Call(Interpreter interpreter, List<IExpression> arguments)
            {
                Turtle.Done();
                return null;
            }
        }

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