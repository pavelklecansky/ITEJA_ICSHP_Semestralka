using System.Collections.Generic;
using Language.Parser.Statement;

namespace Language.Parser
{
    public class Block : IElement
    {
        public List<IStatement> Statements { get; }

        public Block( List<IStatement> statements)
        {
            Statements = statements;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.VisitBlock(this);
        }
    }
}