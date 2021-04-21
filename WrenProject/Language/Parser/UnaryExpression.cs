using Language.Interpreter;
using Language.Lexer;

namespace Language.Parser
{
    /// <summary>
    /// Representation of unary expression
    /// </summary>
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