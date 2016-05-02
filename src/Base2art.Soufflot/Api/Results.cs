namespace Base2art.Soufflot.Api
{
    using System.Net;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;

    public static class Results
    {
        public static IResult Found(this IHttpContext context, string url)
        {
            return context.RedirectInternal(url).WithStatusCode(HttpStatusCode.Found);
        }

        public static IResult Found(this IHttpContext context, IRoute route)
        {
            return context.RedirectInternal(route).WithStatusCode(HttpStatusCode.Found);
        }

        public static IResult MovedPermanently(this IHttpContext context, string url)
        {
            return context.RedirectInternal(url).WithStatusCode(HttpStatusCode.MovedPermanently);
        }

        public static IResult MovedPermanently(this IHttpContext context, IRoute route)
        {
            return context.RedirectInternal(route).WithStatusCode(HttpStatusCode.MovedPermanently);
        }

        public static IResult Redirect(this IHttpContext context, string url)
        {
            return context.RedirectInternal(url).WithStatusCode(HttpStatusCode.Redirect);
        }

        public static IResult Redirect(this IHttpContext context, IRoute route)
        {
            return context.RedirectInternal(route).WithStatusCode(HttpStatusCode.Redirect);
        }

        public static IResult SeeOther(this IHttpContext context, string url)
        {
            return context.RedirectInternal(url).WithStatusCode(HttpStatusCode.SeeOther);
        }

        public static IResult SeeOther(this IHttpContext context, IRoute route)
        {
            return context.RedirectInternal(route).WithStatusCode(HttpStatusCode.SeeOther);
        }

        public static IResult TemporaryRedirect(this IHttpContext context, string url)
        {
            return context.RedirectInternal(url).WithStatusCode(HttpStatusCode.TemporaryRedirect);
        }

        public static IResult TemporaryRedirect(this IHttpContext context, IRoute route)
        {
            return context.RedirectInternal(route).WithStatusCode(HttpStatusCode.TemporaryRedirect);
        }

        private static ResponseResult RedirectInternal(this IHttpContext context, IRoute route)
        {
            return context.RedirectInternal(route.Explode());
        }

        private static ResponseResult RedirectInternal(this IHttpContext context, string url)
        {
            if (context == null)
            {
                throw new System.ArgumentNullException("context");
            }
            
            return new ResponseResult(context.Response, new SimpleContent { BodyContent = "Location: " + url })
                .WithLocation(url);
        }
    }
}
