using Language.Interpreter;

namespace Language.Parser
{
    /// <summary>
    /// Representation of variable
    /// </summary>
    public class Variable : IExpression
    {
        public string Name { get; }

        public Variable(string name)
        {
            Name = name;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.VisitVariable(this);
        }
    }
}