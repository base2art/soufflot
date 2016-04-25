namespace Base2art.Soufflot.Http.Owin
{
    using System;

    using Base2art.Soufflot.Api.Config;

    using global::Owin;

    public static class AppBuilderExtender
    {
        public static string BaseDirectory(this IAppBuilder appBuilder)
        {
            var currentDomain = AppDomain.CurrentDomain;
            var baseDirectory = currentDomain.GetData(CommonSettings.RootDirectoryKey) as string;
            if (baseDirectory != null)
            {
                return baseDirectory;
            }

            return currentDomain.BaseDirectory;
        }
    }
}