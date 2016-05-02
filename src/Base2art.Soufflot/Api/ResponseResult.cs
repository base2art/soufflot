namespace Base2art.Soufflot.Api
{
    using Base2art.Soufflot.Http;

    public class ResponseResult : IResult
    {
        private readonly IHttpResponse response;

        private readonly IContent content;

        private const string DefaultContenType = "text/plain";

        public ResponseResult(IHttpResponse response, IContent content)
        {
            this.response = response;
            this.content = content;
            this.response.SetContentType(GetContentType(content));
        }

        public IContent Content
        {
            get
            {
                return this.content ?? new SimpleContent { BodyContent = string.Empty };
            }
        }

        public IResult As(string newContentType)
        {
            this.response.SetContentType(newContentType);
            return this;
        }

        public ResponseResult WithLocation(string url)
        {
            this.response.SetHeader("Location", url);
            return this;
        }

        public ResponseResult WithStatusCode(System.Net.HttpStatusCode statusCode)
        {
            this.response.SetStatusCode((int)statusCode);
            return this;
        }

        private static string GetContentType(IContent contentValue)
        {
            if (contentValue == null)
            {
                return DefaultContenType;
            }


            return contentValue.ContentType ?? DefaultContenType;
        }
    }
}