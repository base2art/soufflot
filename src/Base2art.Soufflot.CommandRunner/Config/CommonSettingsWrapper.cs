namespace Base2art.Soufflot.CommandRunner.Config
{
    using Base2art.Soufflot.Api.Config;
    using Base2art.Soufflot.Api.Diagnostics;

    public static class CommonSettingsWrapper
    {
        public static readonly string ConsoleLoggerFactoryTypeName = typeof(ConsoleLoggerFactory).AssemblyQualifiedName;

        public const string AppBuilderClassNameKey = CommonSettings.AppBuilderClassNameKey;

        public const string AppModeKey = CommonSettings.AppModeKey;

        public const string LoggerFactoryClassNameKey = CommonSettings.LoggerFactoryClassNameKey;

        public const string LogLevelKey = CommonSettings.LogLevelKey;
        
        public const string RootDirectoryKey = CommonSettings.RootDirectoryKey;
        
        public const string SaltKey = CommonSettings.SaltKey;
    }
}
