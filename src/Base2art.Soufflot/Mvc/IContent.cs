namespace Base2art.Soufflot.Mvc
{
    public interface IContent
    {
        byte[] Body { get; }
        
        string BodyAsString { get; }

        string ContentType { get; }
    }
}