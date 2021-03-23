namespace Language.Parser
{
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