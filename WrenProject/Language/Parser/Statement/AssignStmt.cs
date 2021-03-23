namespace Language.Parser.Statement
{
    public class AssignStmt : IStatement
    {
        public string Name { get; }
        public IExpression Value { get; }

        public AssignStmt(string name, IExpression value)
        {
            Name = name;
            Value = value;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.VisitAssignStmt(this);
        }
    }
}