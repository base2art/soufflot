namespace App.Conf
{
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Config;

    public class ApplicationBuilder : IApplicationBuilder
    {
        public IApplication BuildApplication(ApplicationMode mode, string rootDirectory, IConfigurationProvider configProvider)
        {
            return new Application(mode, rootDirectory, configProvider);
        }

        private class Application : Base2art.PlayN.Api.Application
        {
            public Application(ApplicationMode mode, string rootDirectory, IConfigurationProvider configurationProvider)
                : base(mode, rootDirectory, configurationProvider)
            {
            }

            protected override IRouter CreateRouter()
            {
                return new CustomRoutes();
            }
        }
    }
}
