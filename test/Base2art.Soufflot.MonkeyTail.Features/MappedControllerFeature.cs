namespace Base2art.Soufflot.Pack.Features
{
    using System;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Diagnostics;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Http.Owin;
    using Base2art.Soufflot.Mvc;
    using Base2art.Soufflot.Pack.Features.Fixtures;

    using FluentAssertions;

    using Microsoft.Owin;

    using NUnit.Framework;

    [TestFixture]
    public class MappedControllerFeature
    {
        [Test]
        public void ShouldLoadController()
        {
            var router = FakeRouter.Create(typeof(MappedController).GetClass().As<IRenderingRouted>());
            var a = new RoutedExecutionManager(new Application(ApplicationMode.Prod,Environment.CurrentDirectory, null), router);
            IResult rezult = a.ExecuteRoute(this.CreateContext(a.Application));
            rezult.Content.BodyAsString.Should().Be("Content For @{MonkeyTail}");
        }

        private IHttpContext CreateContext(IApplication application)
        {
            return new HttpContext(application, new NullLogger(), null, new OwinContext(), new HttpContextSettings());
        }
    }
}