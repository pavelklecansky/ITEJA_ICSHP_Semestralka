namespace Language.Parser.Statement
{
    public class WhileStmt : IStatement
    {
        public IExpression Condition { get; }
        public Block Do { get; }

        public WhileStmt(IExpression condition, Block doStatement)
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