namespace Base2art.Soufflot.Http
{
    public interface IHttpRequestBody
    {
        bool IsMaxSizeExceeded { get; }

        byte[] AsRaw();

        string AsText();
    }
}