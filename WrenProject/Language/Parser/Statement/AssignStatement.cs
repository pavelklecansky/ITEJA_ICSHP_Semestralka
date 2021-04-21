using Language.Interpreter;

namespace Language.Parser.Statement
{
    /// <summary>
    /// Representation of assign statement
    /// </summary>
    public class AssignStatement : IStatement
    {
        public string Name { get; }
        public IExpression Value { get; }

        public AssignStatement(string name, IExpression value)
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