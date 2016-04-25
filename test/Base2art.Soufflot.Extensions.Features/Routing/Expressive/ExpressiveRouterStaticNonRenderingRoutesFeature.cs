namespace Base2art.Soufflot.Routing.Expressive
{
    using System.Linq;

    using Base2art.Soufflot.Api.Routing.Expressive;
    using Base2art.Soufflot.Http;

    using FluentAssertions;

    using NUnit.Framework;
	using Base2art.Soufflot.Fixtures;

    [TestFixture]
    public class ExpressiveRouterStaticNonRenderingRoutesFeature : ExpressiveRouterBaseFeature
    {
        [Test]
        public void ShouldRegisterSimpleRouteAndFindItByPath()
        {
            ExpressiveRouter router = new ExpressiveRouter();
            router.RegisterNonRenderingRoute<CustomNonRenderingController>(HttpMethod.Get, null, "/user/edit.html");
            IHttpRequest request = this.CreateRequestFor("/user/edit.html");
            router.FindNonRenderingControllerTypes(request).First().Type.Should().Be(typeof(CustomNonRenderingController));
        }

        [Test]
        public void ShouldRegisterSimpleRouteAndFindItByHostAndPath()
        {
            ExpressiveRouter router = new ExpressiveRouter();
            router.RegisterNonRenderingRoute<CustomNonRenderingController>(HttpMethod.Get, "www.scottyoungblut.com", "/user/edit.html");
            IHttpRequest request = this.CreateRequestFor(HttpMethod.Get, "www.scottyoungblut.com", "/user/edit.html");
            IHttpRequest notFoundRequest = this.CreateRequestFor(HttpMethod.Get, "www.base2art.com", "/user/edit.html");

            router.FindNonRenderingControllerTypes(request).First().Type.Should().Be(typeof(CustomNonRenderingController));
            router.FindNonRenderingControllerTypes(notFoundRequest).Should().BeEmpty();
        }

        [Test]
        public void ShouldRegisterSimpleRouteAndFindItByMethodHostAndPath()
        {
            ExpressiveRouter router = new ExpressiveRouter();
            router.RegisterNonRenderingRoute<CustomNonRenderingController>(HttpMethod.Get, "www.scottyoungblut.com", "/user/edit.html");
            IHttpRequest request = this.CreateRequestFor(HttpMethod.Get, "www.scottyoungblut.com", "/user/edit.html");
            IHttpRequest notFoundRequest = this.CreateRequestFor(HttpMethod.Put, "www.scottyoungblut.com", "/user/edit.html");

            router.FindNonRenderingControllerTypes(request).First().Type.Should().Be(typeof(CustomNonRenderingController));
            router.FindNonRenderingControllerTypes(notFoundRequest).Should().BeEmpty();
        }

        [Test]
        public void ShouldRegisterSimpleRouteAndFindItNoDomainSpecifiedShouldFindRoute()
        {
            ExpressiveRouter router = new ExpressiveRouter();
            router.RegisterNonRenderingRoute<CustomNonRenderingController>(HttpMethod.Get, null, "/user/edit.html");
            IHttpRequest request = this.CreateRequestFor(HttpMethod.Get, "www.scottyoungblut.com", "/user/edit.html");
            IHttpRequest notFoundRequest = this.CreateRequestFor(HttpMethod.Get, "www.base2art.com", "/user/edit.html");

            router.FindNonRenderingControllerTypes(request).First().Type.Should().Be(typeof(CustomNonRenderingController));
            router.FindNonRenderingControllerTypes(notFoundRequest).First().Type.Should().Be(typeof(CustomNonRenderingController));
        }

        [Test]
        public void ShouldRegisterSimpleRouteAndFindItNoMethodSpecifiedShouldFindRoute()
        {
            ExpressiveRouter router = new ExpressiveRouter();
            router.RegisterNonRenderingRoute<CustomNonRenderingController>(null, null, "/user/edit.html");
            IHttpRequest request = this.CreateRequestFor(HttpMethod.Get, "www.scottyoungblut.com", "/user/edit.html");
            IHttpRequest notFoundRequest = this.CreateRequestFor(HttpMethod.Put, "www.base2art.com", "/user/edit.html");

            router.FindNonRenderingControllerTypes(request).First().Type.Should().Be(typeof(CustomNonRenderingController));
            router.FindNonRenderingControllerTypes(notFoundRequest).First().Type.Should().Be(typeof(CustomNonRenderingController));
        }

        [Test]
        public void ShouldGetNullWhenControllerNotFound()
        {
            ExpressiveRouter router = new ExpressiveRouter();
            router.RegisterNonRenderingRoute<CustomNonRenderingController>(HttpMethod.Get, null, "/user/list.html");
            IHttpRequest request = this.CreateRequestFor("/user/edit.html");
            router.FindNonRenderingControllerTypes(request).Should().BeEmpty();
        }
    }
}