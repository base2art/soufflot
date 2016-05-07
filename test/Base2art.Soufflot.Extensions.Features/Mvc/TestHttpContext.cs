namespace Base2art.Soufflot.Mvc
{
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Diagnostics;
    using Base2art.Soufflot.Http;

    public class TestHttpContext : IHttpContext
    {
        private readonly TestHttpResponse response = new TestHttpResponse();

        public IApplication ApplicationInstance
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public ILogger Logger
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        IHttpFlash IHttpContext.Flash
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        IHttpSession IHttpContext.Session
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        IHttpResponse IHttpContext.Response
        {
            get { return this.Response; }
        }

        IHttpRequest IHttpContext.Request
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public TestHttpResponse Response
        {
            get { return this.response; }
        }
    }
}
