using Language.Parser.Statement;

namespace Language.Parser
{
    public interface IVisitor
    {
        object VisitAssignStmt(AssignStatement assignStatement);

        object VisitCallStmt(CallStatement callStatement);
        object VisitIfStmt(IfStatement ifStatement);
        object VisitWhileStmt(WhileStatement whileStatement);
        object VisitBinaryExpr(BinaryExpression binary);
        object VisitBlock(Block block);
        object VisitNumber(Number number);
        object VisitUnaryExpression(UnaryExpression unary);
        object VisitVar(Var var);

        object VisitVariable(Variable variable);

        object VisitStringLiteral(StringLiteral expr);
    }
}