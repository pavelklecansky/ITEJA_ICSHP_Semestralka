using System;
using System.Collections.Generic;
using Language.Parser;

namespace Language.Interpreter.NativeLibrary
{
    /// <summary>
    /// Implementation of native System class. 
    /// </summary>
    internal abstract class SystemClass
    {
        /// <summary>
        /// Write text to standard output followed by newline.
        /// </summary>
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