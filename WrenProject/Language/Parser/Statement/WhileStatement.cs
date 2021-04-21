using Language.Interpreter;

namespace Language.Parser.Statement
{
    /// <summary>
    /// Representation of while statement
    /// </summary>
    public class WhileStatement : IStatement
    {
        public IExpression Condition { get; }
        public Block Do { get; }

        public WhileStatement(IExpression condition, Block doStatement)
        {
            Condition = condition;
            Do = doStatement;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.VisitWhileStmt(this);
        }
    }
}