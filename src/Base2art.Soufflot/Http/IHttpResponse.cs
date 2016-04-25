namespace Base2art.Soufflot.Http
{
    using System;

    public interface IHttpResponse
    {   
        IHttpReadOnlyHeaderCollection Headers { get; }

        void DiscardCookies(params string[] cookieNames);

        void SetContentType(string contentType);

        void SetStatusCode(int statusCode);

        void SetHeader(string name, string value);

        void SetCookie(string name, string value);

        void SetCookie(string name, string value, TimeSpan timeFromNow);

        void SetCookie(string name, string value, TimeSpan timeFromNow, string path);

        void SetCookie(string name, string value, TimeSpan timeFromNow, string path, string domain);

        void SetCookie(string name, string value, TimeSpan timeFromNow, string path, string domain, bool secure, bool httpOnly);
    }
}

// Can't Do this now
//        IHttpCookieCollection Cookies { get; }
//
//        IReadOnlyArrayList<string> DiscardedCookies { get; }
