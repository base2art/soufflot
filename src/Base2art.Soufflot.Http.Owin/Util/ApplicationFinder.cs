namespace Base2art.Soufflot.Http.Owin
{
    using System;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Config;

    public static class ApplicationFinder
    {
        public static IApplication FindApplication(string rootDirectory, IConfigurationProvider configProvider)
        {
            var appModeStr = configProvider.GetValue(CommonSettings.AppModeKey);
            ApplicationMode appMode;
            Enum.TryParse(appModeStr, out appMode);

            var value = configProvider.GetValue(CommonSettings.AppBuilderClassNameKey);
            Type type;
            if (string.IsNullOrWhiteSpace(value))
            {
                type = FindApplicationBuilderTypeByName("App_Code.ApplicationBuilder");
                if (type == null)
                {
                    return new Application(appMode, rootDirectory, configProvider);
                }
            }
            else
            {
                type = FindApplicationBuilderTypeByName(value);
                if (type == null)
                {
                    throw new InvalidOperationException("Type Not Found '" + value + "'");
                }
            }

            var applicationBuilder = (IApplicationBuilder)Activator.CreateInstance(type);
            return applicationBuilder.BuildApplication(appMode, rootDirectory, configProvider);
        }

        private static Type FindApplicationBuilderTypeByName(string value)
        {
            return Type.GetType(value, false);
        }
    }
}
