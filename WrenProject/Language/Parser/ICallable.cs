using System.Collections.Generic;

namespace Language.Parser
{
    public interface ICallable
    {
        object Call(Interpreter interpreter, List<IExpression> arguments);
    }
}