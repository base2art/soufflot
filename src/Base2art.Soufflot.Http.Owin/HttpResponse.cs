namespace Base2art.Soufflot.Http.Owin
{
    using System;
    using System.Collections.Generic;

    using Base2art.Collections;
    using Base2art.Soufflot.Linq;

    using Microsoft.Owin;

    public class HttpResponse : IHttpResponse
    {
        private readonly IOwinResponse response;

        private readonly HttpContextSettings settings;

        public HttpResponse(IOwinResponse response, HttpContextSettings settings)
        {
            this.response = response;
            this.settings = settings;
        }

        public HttpReadOnlyHeaderCollection Headers
        {
            get { return new HttpReadOnlyHeaderCollection(this.response.Headers); }
        }

        IHttpReadOnlyHeaderCollection IHttpResponse.Headers
        {
            get { return this.Headers; }
        }

        public void DiscardCookies(params string[] cookieNames)
        {
            IEnumerable<string> enumerable = cookieNames.Coalesce();
            enumerable.ForAll(x => this.response.Cookies.Delete(x));
        }

        public void SetContentType(string contentType)
        {
            this.response.ContentType = contentType;
        }

        public void SetStatusCode(int statusCode)
        {
            this.response.StatusCode = statusCode;
        }

        public void SetHeader(string name, string value)
        {
            this.ValidateValidCookie(name);
            this.response.Headers.Set(name, value);
        }

        public void SetCookie(string name, string value)
        {
            this.ValidateValidCookie(name);
            this.response.Cookies.Append(name, value);
        }

        public void SetCookie(string name, string value, TimeSpan timeFromNow)
        {
            this.ValidateValidCookie(name);
            this.response.Cookies.Append(name, value, new CookieOptions { Expires = DateTime.Now.Add(timeFromNow) });
        }

        public void SetCookie(string name, string value, TimeSpan timeFromNow, string path)
        {
            this.ValidateValidCookie(name);
            this.response.Cookies.Append(
                name,
                value,
                new CookieOptions { Expires = DateTime.Now.Add(timeFromNow), Path = path });
        }

        public void SetCookie(string name, string value, TimeSpan timeFromNow, string path, string domain)
        {
            this.ValidateValidCookie(name);
            this.response.Cookies.Append(
                name,
                value,
                new CookieOptions { Expires = DateTime.Now.Add(timeFromNow), Path = path, Domain = domain });
        }

        public void SetCookie(string name, string value, TimeSpan timeFromNow, string path, string domain, bool secure, bool httpOnly)
        {
            this.ValidateValidCookie(name);
            this.response.Cookies.Append(
                name,
                value,
                new CookieOptions
                {
                    Expires = DateTime.Now.Add(timeFromNow),
                    Path = path,
                    Domain = domain,
                    Secure = secure,
                    HttpOnly = httpOnly
                });
        }

        private void ValidateValidCookie(string cookieName)
        {
//            if (cookieName == this.settings.FlashCookieName)
//            {
//                throw new InvalidOperationException("Use the Flash API instead of setting this but cookie");
//            }
//
//            if (cookieName == this.settings.SessionCookieName)
//            {
//                throw new InvalidOperationException("Use the Session API instead of setting this but cookie");
//            }
//
//            if (cookieName == this.settings.UserCookieName)
//            {
//                throw new InvalidOperationException("Use the Session API instead of setting this but cookie");
//            }
        }
    }
}

//        IHttpCookieCollection IHttpResponse.Cookies
//        {
//            get
//            {
//                return this.Cookies;
//            }
//        }

//        public HttpCookieCollection Cookies
//        {
//            get
//            {
//                return new HttpCookieCollection(this.response.Cookies, this.settings);
//            }
//        }
