namespace Base2art.Soufflot.Api
{
    public interface IContent
    {
        byte[] Body { get; }
        
        string BodyAsString { get; }

        string ContentType { get; }
    }
}
