using Language.Parser.Statement;

namespace Language.Parser
{
    public interface IVisiter
    {
        object VisitAssignStmt(AssignStmt assignStmt);

        object VisitCallStmt(CallStmt callStmt);
        object VisitIfStmt(IfStmt ifStmt);
        object VisitWhileStmt(WhileStmt whileStmt);
        object VisitBinaryExpr(BinaryExpression binary);
        object VisitBlock(Block block);
        object VisitNumber(Number number);
        object VisitUnaryExpression(UnaryExpression unary);
        object VisitVar(Var var);

        object VisitVariable(Variable variable);

        object VisitStringLiteral(StringLiteral expr);
    }
}