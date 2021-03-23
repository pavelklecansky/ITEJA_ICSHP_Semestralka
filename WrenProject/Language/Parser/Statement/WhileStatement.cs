namespace Language.Parser.Statement
{
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