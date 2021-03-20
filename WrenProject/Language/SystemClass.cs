using System;
using System.Collections.Generic;
using Language.Parser;

namespace Language
{
    class SystemClass
    {
        internal class Print : ICallable
        {
            public object Call(Interpreter interpreter, List<IExpression> arguments)
            {
                if (arguments.Count > 1)
                {
                    throw new ArgumentException("Unexpected expresion.");
                }

                Console.WriteLine(arguments[0].Accept(interpreter));
                return null;
            }
        }
    }
}