using Language.Interpreter;

namespace Language.Parser.Statement
{
    /// <summary>
    /// Representation of if statement
    /// </summary>
    public class IfStatement : IStatement
    {
        public IExpression Condition { get; }
        public Block Then { get; }
        
        public Block Else { get; }

        public IfStatement(IExpression condition, Block then, Block @else)
        {
            Condition = condition;
            Then = then;
            Else = @else;
        }
        
        public object Accept(IVisitor visitor)
        {
            return visitor.VisitIfStmt(this);
        }
        
    }
}