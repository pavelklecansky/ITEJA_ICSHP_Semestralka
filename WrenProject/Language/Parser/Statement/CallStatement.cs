using System.Collections.Generic;
using Language.Interpreter;
using Language.Lexer;

namespace Language.Parser.Statement
{
    /// <summary>
    /// Representation of call statement
    /// </summary>
    public class CallStatement : IStatement
    {
        public Token Class { get; }
        public string Name { get; }
        public List<IExpression> Arguments { get; }

        public CallStatement(Token @class, string name, List<IExpression> arguments)
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