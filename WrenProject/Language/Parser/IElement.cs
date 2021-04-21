using Language.Interpreter;

namespace Language.Parser
{
    /// <summary>
    /// Interface for all elements in interpreter.
    /// </summary>
    public interface IElement
    {
       object Accept(IVisitor visitor);
    }
}