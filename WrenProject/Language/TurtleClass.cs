using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using Language.Parser;


namespace Language
{
    public class TurtleClass
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
                if (arguments.Count == 0)
                {
                    Turtle.Forward();
                }
                else if (arguments.Count == 1)
                {
                    Turtle.Forward((double) arguments[0].Accept(interpreter));
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