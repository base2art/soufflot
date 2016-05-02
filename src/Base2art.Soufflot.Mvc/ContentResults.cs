namespace Base2art.Soufflot.Mvc
{
    using System.Net;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    

    public static class ContentResults
    {
        public static IResult BadRequest(this IHttpContext Routed)
        {
            return Routed.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        public static IResult BadRequest(this IHttpContext Routed, IContent content)
        {
            return Routed.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        public static IResult Created(this IHttpContext Routed)
        {
            return Routed.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.Created);
        }

        public static IResult Created(this IHttpContext Routed, IContent content)
        {
            return Routed.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.Created);
        }

        public static IResult Forbidden(this IHttpContext Routed)
        {
            return Routed.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.Forbidden);
        }

        public static IResult Forbidden(this IHttpContext Routed, IContent content)
        {
            return Routed.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.Forbidden);
        }

        public static IResult InternalServerError(this IHttpContext Routed)
        {
            return Routed.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.InternalServerError);
        }

        public static IResult InternalServerError(this IHttpContext Routed, IContent content)
        {
            return Routed.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.InternalServerError);
        }

        public static IResult NoContent(this IHttpContext Routed)
        {
            return Routed.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.NoContent);
        }

        public static IResult NotFound(this IHttpContext Routed)
        {
            return Routed.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.NotFound);
        }

        public static IResult NotFound(this IHttpContext Routed, IContent content)
        {
            return Routed.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.NotFound);
        }

        public static IResult Ok(this IHttpContext Routed)
        {
            return Routed.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.OK);
        }

        public static IResult Ok(this IHttpContext Routed, IContent content)
        {
            return Routed.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.OK);
        }

        public static IResult Status(this IHttpContext Routed, int status)
        {
            return Routed.ResultInternal(NullContent())
                .WithStatusCode((HttpStatusCode)status);
        }

        public static IResult Status(this IHttpContext Routed, int status, IContent content)
        {
            return Routed.ResultInternal(content)
                .WithStatusCode((HttpStatusCode)status);
        }

        public static IResult Todo(this IHttpContext Routed)
        {
            return Routed.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.NotImplemented);
        }

        public static IResult Unauthorized(this IHttpContext Routed)
        {
            return Routed.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        public static IResult Unauthorized(this IHttpContext Routed, IContent content)
        {
            return Routed.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        private static ResponseResult ResultInternal(this IHttpContext Routed, IContent content)
        {
            return new ResponseResult(Routed.Response, content);
        }

        private static IContent NullContent()
        {
            return new SimpleContent { BodyContent = string.Empty };
        }
    }
}
