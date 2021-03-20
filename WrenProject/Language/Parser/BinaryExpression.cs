namespace Language.Parser
{
    public class BinaryExpression : IExpression
    {
        public IExpression Left { get; }
        public Token Oper { get; }
        public IExpression Right { get; }

        public BinaryExpression(IExpression left, Token oper, IExpression right)
        {
            Left = left;
            Oper = oper;
            Right = right;
        }

        public object Accept(IVisiter visiter)
        {
            return visiter.VisitBinaryExpr(this);
        }
    }
}