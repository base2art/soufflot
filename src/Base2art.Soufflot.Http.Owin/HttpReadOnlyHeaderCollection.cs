namespace Base2art.Soufflot.Http.Owin
{
    using Base2art.Collections;

    using Microsoft.Owin;

    public class HttpReadOnlyHeaderCollection : MultiMap<string, string>, IHttpReadOnlyHeaderCollection
    {
        public HttpReadOnlyHeaderCollection(IHeaderDictionary headers)
        {
            headers.ForAll(header => header.Value.ForAll(headerValue => this.Add(header.Key, headerValue)));
        }
    }
}
