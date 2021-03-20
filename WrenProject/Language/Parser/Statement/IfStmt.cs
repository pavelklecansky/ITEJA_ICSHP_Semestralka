namespace Language.Parser.Statement
{
    public class IfStmt : IStatement
    {
        public IExpression Condition { get; }
        public Block Then { get; }
        
        public Block Else { get; }

        public IfStmt(IExpression condition, Block then, Block @else)
        {
            Condition = condition;
            Then = then;
            Else = @else;
        }
        
        public object Accept(IVisiter visiter)
        {
            return visiter.VisitIfStmt(this);
        }
        
    }
}