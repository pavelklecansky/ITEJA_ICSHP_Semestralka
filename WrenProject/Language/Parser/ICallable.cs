using System.Collections.Generic;

namespace Language.Parser
{
    /// <summary>
    /// Interface for all objects that can by call.
    /// </summary>
    public interface ICallable
    {
        object Call(Interpreter.Interpreter interpreter, List<IExpression> arguments);
    }
}