namespace Base2art.Soufflot.Routing.Functional
{
    using System;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Routing.Functional;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    using FakeItEasy;

    using FluentAssertions;

    using NUnit.Framework;
    using Base2art.Soufflot.Fixtures;

    [TestFixture]
    public class FunctionalRoutingFeature
    {
        [Test]
        public void ShouldExecuteControllerNotFoundByMatch()
        {
            var router = new FunctionalRouter(
                new IRenderingControllerSearchDelegate[] { new FunctionalRenderingControllerSearchDelegate(x => null), },
                null);
            router.FindRenderingControllerType(this.CreateContext("sdf").Request).Should().BeNull();
        }

        [Test]
        public void ShouldExecuteControllerNotFoundByMatch2()
        {
            var router = new FunctionalRouter(
                new IRenderingControllerSearchDelegate[] { new FunctionalRenderingControllerSearchDelegate(null), },
                null);
            router.FindRenderingControllerType(this.CreateContext("sdf").Request).Should().BeNull();
        }

        [Test]
        public void ShouldExecuteControllerNotFoundNoDelegates()
        {
            var router = new FunctionalRouter(null, null);
            router.FindRenderingControllerType(this.CreateContext("sdf").Request).Should().BeNull();
        }

        [Test]
        public void ShouldExecuteControllerFoundByMatch()
        {
            var router = new FunctionalRouter(
                new IRenderingControllerSearchDelegate[] { new FunctionalRenderingControllerSearchDelegate(x => typeof(CustomController)), },
                null);
            router.FindRenderingControllerType(this.CreateContext("sdf").Request).Type.Should().Be(typeof(CustomController));
        }

        [Test]
        public void ShouldNotFindNonRenderingControllerFoundByMatch1()
        {
            var router = new FunctionalRouter(
                new IRenderingControllerSearchDelegate[] { new FunctionalRenderingControllerSearchDelegate(x => typeof(CustomController)), },
                null);
            router.FindNonRenderingControllerTypes(this.CreateContext("sdf").Request).Should().BeEmpty();
        }

        [Test]
        public void ShouldNotFindNonRenderingControllerFoundByMatch2()
        {
            var router = new FunctionalRouter(
                new IRenderingControllerSearchDelegate[] { new FunctionalRenderingControllerSearchDelegate(x => typeof(CustomController)), },
                new INonRenderingControllerSearchDelegate[0]);
            router.FindNonRenderingControllerTypes(this.CreateContext("sdf").Request).Should().BeEmpty();
        }

        [Test]
        public void ShouldNotFindNonRenderingControllerFoundByMatch3()
        {
            var router = new FunctionalRouter(
                new IRenderingControllerSearchDelegate[] { new FunctionalRenderingControllerSearchDelegate(x => typeof(CustomController)), },
                new INonRenderingControllerSearchDelegate[] { null });
            router.FindNonRenderingControllerTypes(this.CreateContext("sdf").Request).Should().BeEmpty();
        }

        [Test]
        public void ShouldNotFindNonRenderingControllerFoundByMatch4()
        {
            var router = new FunctionalRouter(
                new IRenderingControllerSearchDelegate[] { new FunctionalRenderingControllerSearchDelegate(x => typeof(CustomController)), },
                new INonRenderingControllerSearchDelegate[] { new FunctionalNonRenderingControllerSearchDelegate(null), });
            router.FindNonRenderingControllerTypes(this.CreateContext("sdf").Request).Should().BeEmpty();
        }

        [Test]
        public void ShouldFindNonRenderingControllerFoundByMatch()
        {
            var router = new FunctionalRouter(
                new IRenderingControllerSearchDelegate[] { new FunctionalRenderingControllerSearchDelegate(x => typeof(CustomController)), },
                new INonRenderingControllerSearchDelegate[] { new FunctionalNonRenderingControllerSearchDelegate(x=> typeof(CustomNonRenderingController)), });
            router.FindNonRenderingControllerTypes(this.CreateContext("sdf").Request).Should().HaveCount(1);
        }

        protected IHttpContext CreateContext(string path)
        {
            var context = A.Fake<IHttpContext>();
            var request = A.Fake<IHttpRequest>();
            A.CallTo(() => request.Path).Returns(path);
            A.CallTo(() => request.Method).Returns(HttpMethod.Get);
            A.CallTo(() => request.Host).Returns("localhost");
            A.CallTo(() => context.Request).Returns(request);

            return context;
        }
//                new FunctionalRouter(
//                    new[] { new FunctionalRenderingControllerSearchDelegate(x => typeof(CustomController)) },
//                    null);
    }
}

/*
            var a = new ControllerExecutionManager(new Application(), router);
            IResult rezult = a.ExecuteController(this.CreateContext("/abc"));
            rezult.Content.Body.Should().Be("Page Not Found");*/
