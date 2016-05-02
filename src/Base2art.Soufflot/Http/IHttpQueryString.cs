namespace Base2art.Soufflot.Http
{
    using Base2art.Collections;

    public interface IHttpQueryString : IReadOnlyMultiMap<string, string>
    {
        string OriginalValue { get; }
    }
}
