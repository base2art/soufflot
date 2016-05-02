namespace Base2art.Soufflot.Api
{
    using System;

    using Base2art.ComponentModel;
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Diagnostics;
    using Base2art.Soufflot.Http.Owin;
    using Microsoft.Owin;
    using Base2art.Soufflot.Mvc;

    using FluentAssertions;

    using Base2art.Soufflot.Http;

    using NUnit.Framework;
    using Base2art.Soufflot.Api.Fixtures;

    [TestFixture]
    public class ControllerExecutionManagerFeature
    {
        [Test]
        public void ShouldFindControllerActivateAndExecuteIt()
        {
            var router = FakeRouter.Create(typeof(CustomController).GetClass().As<IRenderingRouted>());
            var a = new RoutedExecutionManager(new Application(ApplicationMode.Prod, Environment.CurrentDirectory, null), router);
            IResult rezult = a.ExecuteRoute(this.CreateContext(a.Application));
            rezult.Content.BodyAsString.Should().Be("Something!");
        }

        [Test]
        public void ShouldFindControllerActivateAndExecuteItUsingIoC()
        {
            var router = FakeRouter.Create(typeof(ICustomController).GetClass().As<IRenderingRouted>());
            var a = new RoutedExecutionManager(new CustomApplication(), router);
            IResult rezult = a.ExecuteRoute(this.CreateContext(a.Application));
            rezult.Content.BodyAsString.Should().Be("Something!");
        }

        [Test]
        public void ShouldExecuteNonRenderingController()
        {
            CountingNonRenderingController.Count = 0;
            SubCountingNonRenderingController.Count = 0;

            var router = FakeRouter.Create(
                typeof(CustomController).GetClass().As<IRenderingRouted>(),
                typeof(CountingNonRenderingController).GetClass().As<INonRenderingRouted>()
                );

            var a = new RoutedExecutionManager(new Application(ApplicationMode.Prod, Environment.CurrentDirectory, null), router);
            IResult rezult = a.ExecuteRoute(this.CreateContext(a.Application));
            rezult.Content.BodyAsString.Should().Be("Something!");
            CountingNonRenderingController.Count.Should().Be(1);
            SubCountingNonRenderingController.Count.Should().Be(1);
        }

        [Test]
        public void ShouldNotHaveExceptionOnClazzNotFound()
        {
            var router = FakeRouter.Create(typeof(ICustomController).GetClass().As<IRenderingRouted>());

            var a = new RoutedExecutionManager(new Application(ApplicationMode.Prod, Environment.CurrentDirectory, null), router);
            IResult rezult = a.ExecuteRoute(this.CreateContext(a.Application));
            rezult.Content.BodyAsString.Should().Be("Page Not Found");
        }

        [Test]
        public void ShouldExecuteControllerWithNonRenderingController()
        {
            CountingNonRenderingController.Count = 0;
            SubCountingNonRenderingController.Count = 0;
            var router = FakeRouter.Create(typeof(CustomControllerWithNonRenderingChild).GetClass().As<IRenderingRouted>());

            var a = new RoutedExecutionManager(new Application(ApplicationMode.Prod, Environment.CurrentDirectory, null), router);
            IResult rezult = a.ExecuteRoute(this.CreateContext(a.Application));
            rezult.Content.BodyAsString.Should().Be("Something!");
            CountingNonRenderingController.Count.Should().Be(1);
            SubCountingNonRenderingController.Count.Should().Be(1);
        }

        [Test]
        public void ShouldExecuteControllerWithRenderingChildController()
        {
            var router = FakeRouter.Create(typeof(ParentController).GetClass().As<IRenderingRouted>());

            var a = new RoutedExecutionManager(new Application(ApplicationMode.Prod, Environment.CurrentDirectory, null), router);
            IResult rezult = a.ExecuteRoute(this.CreateContext(a.Application));
            rezult.Content.BodyAsString.Should().Be("Parent Begin->ChildController<- End");
        }

        [Test]
        public void ShouldExecuteControllerAndHaveLogging()
        {
            var router = FakeRouter.Create(typeof(ParentController).GetClass().As<IRenderingRouted>());

            var application = new CustomApplication();

            var inMemoryLogger = new InMemoryLogger(LogLevels.Always);
            application.SetInstance<ILogger>(x => inMemoryLogger);

            var a = new RoutedExecutionManager(application, router);
            IResult rezult = a.ExecuteRoute(this.CreateContext(a.Application, inMemoryLogger));
            rezult.Content.BodyAsString.Should().Be("Parent Begin->ChildController<- End");
            inMemoryLogger.Messages.Length.Should().BeGreaterThan(0);
        }

        [Test]
        public void ShouldExecuteControllerWithHandler()
        {
            {
                var klazz = Class.GetClass<CustomController>();
                var fakeRouteData = new FakeRouteData<IRenderingRouted>(
                    klazz,
                    (x, y, z) => ((CustomController)(x)).View(y, z, 1));
                var router = FakeRouter.CreateData(fakeRouteData);
                var a = new RoutedExecutionManager(new Application(ApplicationMode.Prod, Environment.CurrentDirectory, null), router);
                IResult rezult = a.ExecuteRoute(this.CreateContext(a.Application));
                rezult.Content.BodyAsString.Should().Be("Number i: 1");
            }
            {
                var klazz = Class.GetClass<CustomController>();
                var fakeRouteData = new FakeRouteData<IRenderingRouted>(
                    klazz,
                    (x, y, z) => ((CustomController)(x)).View(y, z, 2));
                var router = FakeRouter.CreateData(fakeRouteData);
                var a = new RoutedExecutionManager(new Application(ApplicationMode.Prod, Environment.CurrentDirectory, null), router);
                IResult rezult = a.ExecuteRoute(this.CreateContext(a.Application));
                rezult.Content.BodyAsString.Should().Be("Number i: 2");
            }
        }

        [Test]
        public void ShouldExecuteControllerNotFound()
        {
            var router = FakeRouter.Create(null);

            var a = new RoutedExecutionManager(new Application(ApplicationMode.Prod, Environment.CurrentDirectory, null), router);
            IResult rezult = a.ExecuteRoute(this.CreateContext(a.Application));
            rezult.Content.BodyAsString.Should().Be("Page Not Found");
        }

        [Test]
        public void ShouldExecuteControllerNotFoundNotPossibleController()
        {
            var router = FakeRouter.Create(Class.GetClass<INotPossibleController>());

            var a = new RoutedExecutionManager(new Application(ApplicationMode.Prod, Environment.CurrentDirectory, null), router);
            IResult rezult = a.ExecuteRoute(this.CreateContext(a.Application));
            rezult.Content.BodyAsString.Should().Be("Page Not Found");
        }

        private IHttpContext CreateContext(
            IApplication application,
            ILogger logger = null)
        {
            return new HttpContext(application, logger ?? new NullLogger(), null, new OwinContext(), new HttpContextSettings());
        }

        public class CustomApplication : Application
        {
            private static IServiceLoaderInjector injector;

            private readonly IBindableAndSealableServiceLoaderInjector tempInjector;

            public CustomApplication(ApplicationMode mode)
                : base(mode, Environment.CurrentDirectory, null)
            {
                this.tempInjector = ServiceLoader.CreateLoader();
            }

            public CustomApplication() : this(ApplicationMode.Prod)
            {
                this.tempInjector = ServiceLoader.CreateLoader();
            }

            protected override void OnApplicationStart()
            {
                base.OnApplicationStart();
                tempInjector.Bind<ICustomController>()
                    .To(new CustomController());
                injector = tempInjector;
            }

            protected override T CreateItemInstance<T>(IClass<T> klazz, bool returnNullOnErrorOrNotFound)
            {
                this.EnsureStartup();
                T rez = default(T);
                try
                {
                    rez = injector.Resolve(klazz);
                }
                catch (Exception)
                {
                }
                return rez ?? base.CreateItemInstance(klazz, true);
            }

            public void SetInstance<T>(Func<IServiceLoaderInjector, T> func)
            {
                this.tempInjector.Bind<T>()
                    .To(func);
            }
        }
    }
}
