namespace Language.Parser
{
    public interface IElement
    {
       object Accept(IVisitor visitor);
    }
}