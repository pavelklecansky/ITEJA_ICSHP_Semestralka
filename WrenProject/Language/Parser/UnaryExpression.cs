namespace Language.Parser
{
    public class UnaryExpression : IExpression
    {
        public Token Oper { get; }
        public IExpression Right { get; }

        public UnaryExpression(Token oper, IExpression right)
        {
            Oper = oper;
            Right = right;
        }

        public object Accept(IVisiter visiter)
        {
            return visiter.VisitUnaryExpression(this);
        }
    }
}