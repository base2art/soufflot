namespace Base2art.Soufflot.Http.Owin
{
	using System;
    using System.Linq;

    using Base2art.Collections;

    using Microsoft.Owin;

    public class HttpCookieCollection : KeyedCollection<string, IHttpCookie>, IHttpCookieCollection
    {
        public HttpCookieCollection(RequestCookieCollection cookies, HttpContextSettings settings)
            : base(cookie => cookie.Name)
        {
			var str = string.Format("/{0}/", settings.SecureCookiePrefix);
            cookies.Where(cookie => !cookie.Value.StartsWith(str, StringComparison.Ordinal))
                .Select(x=> new HttpCookie(x.Key, x.Value))
                .ToList()
                .ForEach(this.Add);
        }
    }
}