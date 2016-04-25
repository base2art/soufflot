namespace Base2art.Soufflot.Routing.Expressive
{
    using Base2art.Soufflot.Http;

    using FakeItEasy;

    public class ExpressiveRouterBaseFeature
    {
        protected IHttpRequest CreateRequestFor(string path)
        {
            return this.CreateRequestFor(HttpMethod.Get, "localhost", path);
        }

        protected IHttpRequest CreateRequestFor(HttpMethod method, string host, string path)
        {
            var request = A.Fake<IHttpRequest>();
            A.CallTo(() => request.Path).Returns(path);
            A.CallTo(() => request.Method).Returns(method);
            A.CallTo(() => request.Host).Returns(host);

            return request;
        }
    }
}