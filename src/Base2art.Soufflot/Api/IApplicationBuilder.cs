namespace Base2art.Soufflot.Api
{
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Config;

    public interface IApplicationBuilder
    {
        IApplication BuildApplication(ApplicationMode mode, string rootDirectory, IConfigurationProvider configProvider);
    }
}
