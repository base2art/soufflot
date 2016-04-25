namespace Base2art.Soufflot.Mvc
{
    using System.Net;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    

    public static class ContentResults
    {
        public static IResult BadRequest(this IHttpContext controller)
        {
            return controller.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        public static IResult BadRequest(this IHttpContext controller, IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        public static IResult Created(this IHttpContext controller)
        {
            return controller.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.Created);
        }

        public static IResult Created(this IHttpContext controller, IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.Created);
        }

        public static IResult Forbidden(this IHttpContext controller)
        {
            return controller.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.Forbidden);
        }

        public static IResult Forbidden(this IHttpContext controller, IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.Forbidden);
        }

        public static IResult InternalServerError(this IHttpContext controller)
        {
            return controller.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.InternalServerError);
        }

        public static IResult InternalServerError(this IHttpContext controller, IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.InternalServerError);
        }

        public static IResult NoContent(this IHttpContext controller)
        {
            return controller.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.NoContent);
        }

        public static IResult NotFound(this IHttpContext controller)
        {
            return controller.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.NotFound);
        }

        public static IResult NotFound(this IHttpContext controller, IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.NotFound);
        }

        public static IResult Ok(this IHttpContext controller)
        {
            return controller.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.OK);
        }

        public static IResult Ok(this IHttpContext controller, IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.OK);
        }

        public static IResult Status(this IHttpContext controller, int status)
        {
            return controller.ResultInternal(NullContent())
                .WithStatusCode((HttpStatusCode)status);
        }

        public static IResult Status(this IHttpContext controller, int status, IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode((HttpStatusCode)status);
        }

        public static IResult Todo(this IHttpContext controller)
        {
            return controller.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.NotImplemented);
        }

        public static IResult Unauthorized(this IHttpContext controller)
        {
            return controller.ResultInternal(NullContent())
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        public static IResult Unauthorized(this IHttpContext controller, IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        private static ResponseResult ResultInternal(this IHttpContext controller, IContent content)
        {
            return new ResponseResult(controller.Response, content);
        }

        private static IContent NullContent()
        {
            return new SimpleContent { BodyContent = string.Empty };
        }
    }
}
