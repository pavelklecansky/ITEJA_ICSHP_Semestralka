namespace Language.Parser
{
    public class BinaryExpression : IExpression
    {
        public IExpression Left { get; }
        public Token Operator { get; }
        public IExpression Right { get; }

        public BinaryExpression(IExpression left, Token @operator, IExpression right)
        {
            Left = left;
            Operator = @operator;
            Right = right;
        }

        public object Accept(IVisitor visitor)
        {
            return visitor.VisitBinaryExpr(this);
        }
    }
}