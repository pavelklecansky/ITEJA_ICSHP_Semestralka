namespace Language.Parser
{
    public class UnaryExpression : IExpression
    {
        public Token Operator { get; }
        public IExpression Right { get; }

        public UnaryExpression(Token @operator, IExpression right)
        {
            Operator = @operator;
            Right = right;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.VisitUnaryExpression(this);
        }
    }
}