namespace Base2art.Soufflot.Routing.Expressive
{
    using Base2art.Soufflot.Api.Routing.Expressive;
    using Base2art.Soufflot.Http;

    using FluentAssertions;

    using NUnit.Framework;
	using Base2art.Soufflot.Fixtures;

    [TestFixture]
    public class ExpressiveRouterStaticRenderingRoutesFeature : ExpressiveRouterBaseFeature
    {
        [Test]
        public void ShouldRegisterSimpleRouteAndFindItByPath()
        {
            ExpressiveRouter router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(HttpMethod.Get, null, "/user/edit.html");
            IHttpRequest request = this.CreateRequestFor("/user/edit.html");
            router.FindRenderingControllerType(request).Type.Should().Be(typeof(CustomController));
        }

        [Test]
        public void ShouldRegisterSimpleRouteAndFindItByHostAndPath()
        {
            ExpressiveRouter router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(HttpMethod.Get, "www.scottyoungblut.com", "/user/edit.html");
            IHttpRequest request = this.CreateRequestFor(HttpMethod.Get, "www.scottyoungblut.com", "/user/edit.html");
            IHttpRequest notFoundRequest = this.CreateRequestFor(HttpMethod.Get, "www.base2art.com", "/user/edit.html");

            router.FindRenderingControllerType(request).Type.Should().Be(typeof(CustomController));
            router.FindRenderingControllerType(notFoundRequest).Should().BeNull();
        }

        [Test]
        public void ShouldRegisterSimpleRouteAndFindItByMethodHostAndPath()
        {
            ExpressiveRouter router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(HttpMethod.Get, "www.scottyoungblut.com", "/user/edit.html");
            IHttpRequest request = this.CreateRequestFor(HttpMethod.Get, "www.scottyoungblut.com", "/user/edit.html");
            IHttpRequest notFoundRequest = this.CreateRequestFor(HttpMethod.Put, "www.scottyoungblut.com", "/user/edit.html");

            router.FindRenderingControllerType(request).Type.Should().Be(typeof(CustomController));
            router.FindRenderingControllerType(notFoundRequest).Should().BeNull();
        }

        [Test]
        public void ShouldRegisterSimpleRouteAndFindItNoDomainSpecifiedShouldFindRoute()
        {
            ExpressiveRouter router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(HttpMethod.Get, null, "/user/edit.html");
            IHttpRequest request = this.CreateRequestFor(HttpMethod.Get, "www.scottyoungblut.com", "/user/edit.html");
            IHttpRequest notFoundRequest = this.CreateRequestFor(HttpMethod.Get, "www.base2art.com", "/user/edit.html");

            router.FindRenderingControllerType(request).Type.Should().Be(typeof(CustomController));
            router.FindRenderingControllerType(notFoundRequest).Type.Should().Be(typeof(CustomController));
        }

        [Test]
        public void ShouldRegisterSimpleRouteAndFindItNoMethodSpecifiedShouldFindRoute()
        {
            ExpressiveRouter router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(null, null, "/user/edit.html");
            IHttpRequest request = this.CreateRequestFor(HttpMethod.Get, "www.scottyoungblut.com", "/user/edit.html");
            IHttpRequest notFoundRequest = this.CreateRequestFor(HttpMethod.Put, "www.base2art.com", "/user/edit.html");

            router.FindRenderingControllerType(request).Type.Should().Be(typeof(CustomController));
            router.FindRenderingControllerType(notFoundRequest).Type.Should().Be(typeof(CustomController));
        }

        [Test]
        public void ShouldGetNullWhenControllerNotFound()
        {
            ExpressiveRouter router = new ExpressiveRouter();
            router.RegisterRoute<CustomController>(HttpMethod.Get, null, "/user/list.html");
            IHttpRequest request = this.CreateRequestFor("/user/edit.html");
            router.FindRenderingControllerType(request).Should().BeNull();
        }
    }
}