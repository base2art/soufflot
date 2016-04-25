namespace Base2art.Soufflot.Mvc
{
    using System.Net;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;

    public static class Results
    {
        public static IResult Found(this IHttpContext controller, string url)
        {
            return controller.RedirectInternal(url).WithStatusCode(HttpStatusCode.Found);
        }

        public static IResult Found(this IHttpContext controller, IRoute route)
        {
            return controller.RedirectInternal(route).WithStatusCode(HttpStatusCode.Found);
        }

        public static IResult MovedPermanently(this IHttpContext controller, string url)
        {
            return controller.RedirectInternal(url).WithStatusCode(HttpStatusCode.MovedPermanently);
        }

        public static IResult MovedPermanently(this IHttpContext controller, IRoute route)
        {
            return controller.RedirectInternal(route).WithStatusCode(HttpStatusCode.MovedPermanently);
        }

        public static IResult Redirect(this IHttpContext controller, string url)
        {
            return controller.RedirectInternal(url).WithStatusCode(HttpStatusCode.Redirect);
        }

        public static IResult Redirect(this IHttpContext controller, IRoute route)
        {
            return controller.RedirectInternal(route).WithStatusCode(HttpStatusCode.Redirect);
        }

        public static IResult SeeOther(this IHttpContext controller, string url)
        {
            return controller.RedirectInternal(url).WithStatusCode(HttpStatusCode.SeeOther);
        }

        public static IResult SeeOther(this IHttpContext controller, IRoute route)
        {
            return controller.RedirectInternal(route).WithStatusCode(HttpStatusCode.SeeOther);
        }

        public static IResult TemporaryRedirect(this IHttpContext controller, string url)
        {
            return controller.RedirectInternal(url).WithStatusCode(HttpStatusCode.TemporaryRedirect);
        }

        public static IResult TemporaryRedirect(this IHttpContext controller, IRoute route)
        {
            return controller.RedirectInternal(route).WithStatusCode(HttpStatusCode.TemporaryRedirect);
        }

        private static ResponseResult RedirectInternal(this IHttpContext controller, IRoute route)
        {
            return controller.RedirectInternal(route.Explode());
        }

        private static ResponseResult RedirectInternal(this IHttpContext controller, string url)
        {
            return new ResponseResult(controller.Response, new SimpleContent { BodyContent = "Location: " + url })
                .WithLocation(url);
        }
    }
}
