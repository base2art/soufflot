namespace Base2art.Soufflot.Mvc
{
    using System.Net;

    using System.Text;
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;

    public static class MonketTailContentMapper
    {
        public static IResult BadRequest(this IHttpContext controller, MonkeyTail.IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        public static IResult Created(this IHttpContext controller, MonkeyTail.IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.Created);
        }

        public static IResult Forbidden(this IHttpContext controller, MonkeyTail.IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.Forbidden);
        }

        public static IResult InternalServerError(this IHttpContext controller, MonkeyTail.IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.InternalServerError);
        }

        public static IResult NotFound(this IHttpContext controller, MonkeyTail.IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.NotFound);
        }

        public static IResult Ok(this IHttpContext controller, MonkeyTail.IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.OK);
        }

        public static IResult Status(this IHttpContext controller, int status, MonkeyTail.IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode((HttpStatusCode)status);
        }

        public static IResult Unauthorized(this IHttpContext controller, MonkeyTail.IContent content)
        {
            return controller.ResultInternal(content)
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        private static ResponseResult ResultInternal(this IHttpContext controller, MonkeyTail.IContent content)
        {
            return new ResponseResult(controller.Response, new WrappedContent(content));
        }

        private class WrappedContent : IContent
        {
            private readonly MonkeyTail.IContent content;

            public WrappedContent(MonkeyTail.IContent content)
            {
                this.content = content;
            }

            byte[] IContent.Body
            {
                get
                {
                    return Encoding.Default.GetBytes(this.content.Body);
                }
            }

            string IContent.BodyAsString
            {
                get
                {
                    return this.content.Body;
                }
            }

            string IContent.ContentType
            {
                get { return this.content.ContentType; }
            }

            /*
            string Base2art.MonkeyTail.IContent.ContentType
            {
                get { return this.content.ContentType; }
            }

            string Base2art.MonkeyTail.IContent.Body
            {
                get
                {
                    return this.content.Body;
                }
            }
            
            TType Base2art.MonkeyTail.IAppendable<TType>.Append(TType paramT)
            {
                throw new System.NotImplementedException();
            }
            */
        }
    }
}
