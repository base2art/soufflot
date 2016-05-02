namespace Base2art.Soufflot.Http
{
    using Base2art.Collections;

    public interface IHttpCookieCollection : IReadOnlyKeyedCollection<string, IHttpCookie>
    {
    }
}
