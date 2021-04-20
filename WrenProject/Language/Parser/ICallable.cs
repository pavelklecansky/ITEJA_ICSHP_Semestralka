using System.Collections.Generic;

namespace Language.Parser
{
    public interface ICallable
    {
        object Call(Interpreter.Interpreter interpreter, List<IExpression> arguments);
    }
}