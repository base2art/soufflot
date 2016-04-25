namespace Base2art.Soufflot.Api.Config
{
    using System;

    public class AppDomainDataConfigurationProvider : IConfigurationProvider
    {
        private readonly AppDomain targetDomain;

        public AppDomainDataConfigurationProvider(AppDomain targetDomain)
        {
            this.targetDomain = targetDomain;
        }

        public string GetValue(string key)
        {
            return this.targetDomain.GetData(key) as string;
        }
    }
}