using System.Collections.Generic;
using Language.Interpreter;
using Language.Parser.Statement;

namespace Language.Parser
{
    /// <summary>
    /// Representation of block
    /// </summary>
    public class Block : IElement
    {
        public IList<IStatement> Statements { get; }

        public Block(IList<IStatement> statements)
        {
            Statements = statements;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.VisitBlock(this);
        }
    }
}