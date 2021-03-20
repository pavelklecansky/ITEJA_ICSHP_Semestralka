namespace Language.Parser.Statement
{
    public class Var : IStatement
    {
        public string Name { get; }
        public IExpression Value { get; }

        public Var(string name, IExpression value)
        {
            this.Name = name;
            this.Value = value;
        }

        public object Accept(IVisiter visiter)
        {
            return visiter.VisitVar(this);
        }
    }
}