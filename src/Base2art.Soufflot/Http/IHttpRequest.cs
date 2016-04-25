namespace Base2art.Soufflot.Http
{
    using System;

    public interface IHttpRequest
    {
        IHttpCookieCollection Cookies { get; }

        IHttpReadOnlyHeaderCollection Headers { get; }

        HttpMethod Method { get; }

        Uri Uri { get; }

        string Host { get; }

        string Path { get; }

        IHttpQueryString QueryString { get; }

        IHttpUser User { get; }

        IHttpRequestBody RequestBody { get; }
    }
}