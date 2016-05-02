namespace Base2art.Soufflot.Http.Owin
{
    using System;
    using Base2art.Soufflot.Api.Config;
    
    // NOT TO BE USED IN PRODUCTION!!!
    public class TestStartup : StartupBase
    {
        protected override IConfigurationProvider CreateConfiguration(string applicationPath)
        {
            return this.CreateTestConfig(applicationPath);
        }

        public IConfigurationProvider CreateTestConfig(string applicationPath)
        {
            return new ConfigurationProvider(applicationPath);
        }

        private class ConfigurationProvider : IConfigurationProvider
        {
            private readonly string rootDir;

            public ConfigurationProvider(string rootDir)
            {
                this.rootDir = rootDir;
            }

            public string GetValue(string key)
            {
                if (key == CommonSettings.AppModeKey)
                {
                    return "Test";
                }
                if (key == CommonSettings.SaltKey)
                {
                    return "23fcbedb-5b64-490d-977e-5731a0111ed2";
                }
                if (key == CommonSettings.RootDirectoryKey)
                {
                    return this.rootDir;
                }
                return null;
            }
        }
    }
}
