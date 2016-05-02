namespace Base2art.Soufflot.Api
{
    using System;
    using System.Collections.Generic;

    using Base2art.Soufflot.Api.Config;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class ApplicationFeature : IConfigurationProvider
    {
        [Test]
        public void ShouldGetConfiguration()
        {
            IApplication app = new CustomApplication(this);
            app.ConfigurationValue("A").Should().Be("A");

            app = new CustomApplication();
            new Action(() => app.ConfigurationValue("A")).ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void ShouldCreateRouter()
        {
            IApplication app = new CustomApplication(this);
            app.CreateRouter().Should().NotBeNull().And.BeAssignableTo<NullRouter>();
        }

        [Test]
        public void ShouldCreateRouterViaIoC()
        {
            IApplication app = new IoCApplication();
            app.CreateRouter().Should().NotBeNull().And.BeAssignableTo<CustomRouter>();
        }

        [Test]
        public void ShouldReturnNullOrErrorOnIocCorrectly()
        {
            IApplication app = new IoCApplication();
            app.CreateInstance(Class.GetClass<IConfigurationProvider>(), true);
            new Action(() => app.CreateInstance(Class.GetClass<IConfigurationProvider>(), false)).ShouldThrow<Exception>();
            app.Mode.Should().Be(ApplicationMode.Test);
            app.RootDirectory.Should().Be(Environment.CurrentDirectory);
        }

        string IConfigurationProvider.GetValue(string key)
        {
            return key;
        }

        private class CustomApplication : Application
        {
            public CustomApplication()
                : this(null)
            {
            }

            public CustomApplication(IConfigurationProvider configuration)
                : base(ApplicationMode.Test, Environment.CurrentDirectory, configuration)
            {
            }
        }

        private class IoCApplication : Application
        {
            public IoCApplication()
                : base(ApplicationMode.Test, Environment.CurrentDirectory, null)
            {
            }

            protected override T CreateItemInstance<T>(IClass<T> type, bool returnNullOnErrorOrNotFound)
            {
                if (type.Type == typeof(IRouter))
                {
                    return (T)(object)new CustomRouter();
                }

                return base.CreateItemInstance(type, returnNullOnErrorOrNotFound);
            }
        }

        private class CustomRouter : IRouter
        {
            public IRouteData<IRenderingRouted> FindRenderingControllerType(IHttpRequest request)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<IRouteData<INonRenderingRouted>> FindNonRenderingControllerTypes(IHttpRequest request)
            {
                throw new NotImplementedException();
            }
        }
    }
}
