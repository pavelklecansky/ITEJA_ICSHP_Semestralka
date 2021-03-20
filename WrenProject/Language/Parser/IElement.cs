namespace Language.Parser
{
    public interface IElement
    {
       object Accept(IVisiter visiter);
    }
}