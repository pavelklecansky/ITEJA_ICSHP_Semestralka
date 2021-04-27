using Language.Interpreter;

namespace Language.Parser
{
    /// <summary>
    /// Interface for all elements in parser.
    /// </summary>
    public interface IElement
    {
       object Accept(IVisitor visitor);
    }
}