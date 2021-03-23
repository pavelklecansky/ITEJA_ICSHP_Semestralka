using System.Collections.Generic;
using Language.Parser.Statement;

namespace Language.Parser
{
    public class CallStmt : IStatement
    {
        public Token Class { get; }
        public string Name { get; }
        public List<IExpression> Arguments { get; }

        public CallStmt(Token @class, string name, List<IExpression> arguments)
        {
            Class = @class;
            Name = name;
            Arguments = arguments;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.VisitCallStmt(this);
        }
    }
}