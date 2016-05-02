[assembly: Microsoft.Owin.OwinStartup(typeof(Base2art.Soufflot.Http.Owin.Startup))]
namespace Base2art.Soufflot.Http.Owin
{
    using System;
    using System.IO;
    using Base2art.Soufflot.Api.Config;

    public class Startup : StartupBase
    {
        protected override IConfigurationProvider CreateConfiguration(string applicationPath)
        {
            string propsPath = Path.Combine(applicationPath, "App.props");
            return new AggregateConfigurationProvider(
                new PropertiesConfigurationProvider(propsPath),
                new AppDomainDataConfigurationProvider(AppDomain.CurrentDomain));
        }
    }
}
