namespace Base2art.Soufflot.Api
{
    using System.Net;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    
    public static class ContentResults
    {
        public static IResult BadRequest(this IHttpContext context)
        {
            return context.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        public static IResult BadRequest(this IHttpContext context, IContent content)
        {
            return context.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        public static IResult Created(this IHttpContext context)
        {
            return context.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.Created);
        }

        public static IResult Created(this IHttpContext context, IContent content)
        {
            return context.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.Created);
        }

        public static IResult Forbidden(this IHttpContext context)
        {
            return context.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.Forbidden);
        }

        public static IResult Forbidden(this IHttpContext context, IContent content)
        {
            return context.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.Forbidden);
        }

        public static IResult InternalServerError(this IHttpContext context)
        {
            return context.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.InternalServerError);
        }

        public static IResult InternalServerError(this IHttpContext context, IContent content)
        {
            return context.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.InternalServerError);
        }

        public static IResult NoContent(this IHttpContext context)
        {
            return context.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.NoContent);
        }

        public static IResult NotFound(this IHttpContext context)
        {
            return context.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.NotFound);
        }

        public static IResult NotFound(this IHttpContext context, IContent content)
        {
            return context.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.NotFound);
        }

        public static IResult Ok(this IHttpContext context)
        {
            return context.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.OK);
        }

        public static IResult Ok(this IHttpContext context, IContent content)
        {
            return context.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.OK);
        }

        public static IResult Status(this IHttpContext context, int status)
        {
            return context.ResultInternal(NullContent())
                .WithStatusCode((HttpStatusCode)status);
        }

        public static IResult Status(this IHttpContext context, int status, IContent content)
        {
            return context.ResultInternal(content)
                .WithStatusCode((HttpStatusCode)status);
        }

        public static IResult Todo(this IHttpContext context)
        {
            return context.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.NotImplemented);
        }

        public static IResult Unauthorized(this IHttpContext context)
        {
            return context.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        public static IResult Unauthorized(this IHttpContext context, IContent content)
        {
            return context.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        private static ResponseResult ResultInternal(this IHttpContext context, IContent content)
        {
            return new ResponseResult(context.Response, content);
        }

        private static IContent NullContent()
        {
            return new SimpleContent { BodyContent = string.Empty };
        }
    }
}
