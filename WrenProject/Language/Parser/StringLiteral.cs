namespace Language.Parser
{
    public class StringLiteral : IExpression
    {
        public string Value { get; }

        public StringLiteral(string value)
        {
            Value = value;
        }

        public object Accept(IVisiter visiter)
        {
            return visiter.VisitStringLiteral(this);
        }
    }
}