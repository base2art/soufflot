namespace Base2art.Soufflot.Http.Owin
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Owin;

    public static class OwinExtender
    {
        public static OwinContext CreateRequestForPath(string path)
        {
            var ctx = new OwinContext();
            ctx.Request.SetupUrl("http://localhost" + path);
            return ctx;
        }

        public static void SetCookies(this OwinContext context, string[] serCookies)
        {
            //            context.Request.Cookies.Should().NotBeNull();

            IDictionary<string, string[]> headers = context.Request.Headers;
            headers["Cookie"] = serCookies;
        }

        public static string[] GetCookies(this OwinContext context)
        {
            IDictionary<string, string[]> cookieValues = context.Response.Headers;
            var serCookies = cookieValues["Set-Cookie"];
            return serCookies;
        }

        public static void SetupUrl(this IOwinRequest request, string uri)
        {
            var url = new UriBuilder(uri);
            request.Scheme = url.Scheme;
            request.Host = new HostString(url.Host);
            request.Path = new PathString(url.Path);
            request.QueryString = new QueryString(url.Query.TrimStart('?'));
        }
    }
}
