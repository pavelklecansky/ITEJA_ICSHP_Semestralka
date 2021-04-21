using Language.Interpreter;

namespace Language.Parser.Statement
{
    /// <summary>
    /// Representation of creating variable
    /// </summary>
    public class Var : IStatement
    {
        public string Name { get; }
        public IExpression Value { get; }

        public Var(string name, IExpression value)
        {
            Name = name;
            Value = value;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.VisitVar(this);
        }
    }
}