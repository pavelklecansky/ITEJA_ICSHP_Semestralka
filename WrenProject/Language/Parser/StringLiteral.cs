using Language.Interpreter;

namespace Language.Parser
{
    /// <summary>
    /// Representation of string
    /// </summary>
    public class StringLiteral : IExpression
    {
        public string Value { get; }

        public StringLiteral(string value)
        {
            Value = value;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.VisitStringLiteral(this);
        }
    }
}