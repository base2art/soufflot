namespace Base2art.Soufflot.Http.Owin
{
    using System;

    using System.Security.Principal;
    using Microsoft.Owin;

    public class HttpRequest : IHttpRequest
    {
        private readonly IOwinRequest request;

        private readonly HttpContextSettings settings;

        private readonly IHttpUserLookup userLookup;

        private OneTryLazy<IHttpUser> currentUser;

        public HttpRequest(IHttpUserLookup userLookup, IOwinRequest request, HttpContextSettings settings)
        {
            this.request = request;
            this.settings = settings;
            this.userLookup = userLookup;
            this.currentUser = new OneTryLazy<IHttpUser>(() => new HttpUser(HttpUser.NullUserName));
        }

        IHttpRequestBody IHttpRequest.RequestBody
        {
            get
            {
                return this.RequestBody;
            }
        }

        public HttpRequestBody RequestBody
        {
            get
            {
                return new HttpRequestBody(this.request, this.settings);
            }
        }

        public HttpCookieCollection Cookies
        {
            get
            {
                return new HttpCookieCollection(this.request.Cookies, settings);
            }
        }

        IHttpCookieCollection IHttpRequest.Cookies
        {
            get
            {
                return this.Cookies;
            }
        }

        public HttpReadOnlyHeaderCollection Headers
        {
            get
            {
                return new HttpReadOnlyHeaderCollection(this.request.Headers);
            }
        }

        IHttpReadOnlyHeaderCollection IHttpRequest.Headers
        {
            get
            {
                return this.Headers;
            }
        }

        public Uri Uri
        {
            get
            {
                return this.request.Uri;
            }
        }

        public string Host
        {
            get
            {
                return this.Uri.Host;
            }
        }

        public string Path
        {
            get
            {
                return this.Uri.AbsolutePath;
            }
        }

        public IHttpQueryString QueryString
        {
            get
            {
                return new HttpQueryString(this.Uri.Query ?? string.Empty);
            }
        }

        public HttpMethod Method
        {
            get
            {
                return ParseMethod(this.request.Method);
            }
        }

        public IHttpUser User
        {
            get
            {
                return this.currentUser.Value;
            }
        }
        
        public void SetUser(IPrincipal value)
        {
            this.currentUser = new OneTryLazy<IHttpUser>(() => this.FindUser(value));
        }

        private HttpMethod ParseMethod(string method)
        {
            HttpMethod methodValue;
            Enum.TryParse(method, true, out methodValue);
            return methodValue;
        }

        private IHttpUser FindUser(IPrincipal principal)
        {
            if (this.userLookup != null && principal != null && principal.Identity != null && principal.Identity.IsAuthenticated && !string.IsNullOrWhiteSpace(principal.Identity.Name))
            {
                return this.userLookup.FindUser(principal);
            }

            return new HttpUser(HttpUser.NullUserName);
        }
    }
}
