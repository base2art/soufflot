namespace Base2art.Soufflot.Api
{
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class RouteFeature
    {
        [Test]
        public void ShouldLoad()
        {
            var route = new Route("/abc");
            route.Explode().Should().Be("/abc");
            route.ToString().Should().Be("/abc");

            var route1 = new Route(HttpMethod.Get, "www.base2art.com", "/abc");
            route1.Explode().Should().Be("http://www.base2art.com:80/abc");
            route1.ToString().Should().Be("http://www.base2art.com:80/abc");
        }
    }
}
