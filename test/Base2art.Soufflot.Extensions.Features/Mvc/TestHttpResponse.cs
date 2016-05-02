namespace Base2art.Soufflot.Mvc
{
    using System;

    using Base2art.Collections;
    using Base2art.Soufflot.Http;

    public class TestHttpResponse : IHttpResponse
    {
        private readonly HeaderCollection coll = new HeaderCollection();

        public int StatusCode { get; set; }
        public string ContentType { get; set; }

        public IHttpReadOnlyHeaderCollection Headers
        {
            get
            {
                return this.coll;
            }
        }

        public void DiscardCookies(params string[] cookieNames)
        {
            throw new NotImplementedException();
        }

        public void SetContentType(string contentType)
        {
            this.ContentType = contentType;
        }

        public void SetStatusCode(int statusCode)
        {
            this.StatusCode = statusCode;
        }

        public void SetHeader(string name, string value)
        {
            this.coll.Add(name, value);
        }

        public void SetCookie(string name, string value)
        {
            throw new NotImplementedException();
        }

        public void SetCookie(string name, string value, TimeSpan timeFromNow)
        {
            throw new NotImplementedException();
        }

        public void SetCookie(string name, string value, TimeSpan timeFromNow, string path)
        {
            throw new NotImplementedException();
        }

        public void SetCookie(string name, string value, TimeSpan timeFromNow, string path, string domain)
        {
            throw new NotImplementedException();
        }

        public void SetCookie(string name, string value, TimeSpan timeFromNow, string path, string domain, bool secure, bool httpOnly)
        {
            throw new NotImplementedException();
        }

        private class HeaderCollection : MultiMap<string,string>,IHttpReadOnlyHeaderCollection
        {
        }
    }
}
