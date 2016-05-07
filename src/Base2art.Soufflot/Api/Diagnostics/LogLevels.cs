namespace Base2art.Soufflot.Api.Diagnostics
{
    public static class LogLevels
    {
        public static readonly LogLevel Always = new LogLevel(PredefinedLogLevel.Always);

        // 1-63
        // 0x01 - 0x3F
        public static readonly LogLevel SystemEmergencyEvent = new LogLevel(PredefinedLogLevel.SystemEmergencyEvent);

        public static readonly LogLevel SystemFatalEvent = new LogLevel(PredefinedLogLevel.SystemFatalEvent);

        public static readonly LogLevel SystemAlertEvent = new LogLevel(PredefinedLogLevel.SystemAlertEvent);

        public static readonly LogLevel SystemCriticalEvent = new LogLevel(PredefinedLogLevel.SystemCriticalEvent);

        public static readonly LogLevel SystemSevereEvent = new LogLevel(PredefinedLogLevel.SystemSevereEvent);

        // 64 - 127
        // 0x40 - 0x7F
        public static readonly LogLevel ApplicationError = new LogLevel(PredefinedLogLevel.ApplicationError);

        public static readonly LogLevel ApplicationWarn = new LogLevel(PredefinedLogLevel.ApplicationWarn);

        public static readonly LogLevel ApplicationDebug = new LogLevel(PredefinedLogLevel.ApplicationDebug);

        public static readonly LogLevel ApplicationNotice = new LogLevel(PredefinedLogLevel.ApplicationNotice);

        public static readonly LogLevel ApplicationInfo = new LogLevel(PredefinedLogLevel.ApplicationInfo);

        // 128 - 191
        // 0x80 - 0xBF
        public static readonly LogLevel DeveloperTrace = new LogLevel(PredefinedLogLevel.DeveloperTrace);

        public static readonly LogLevel DeveloperFiner = new LogLevel(PredefinedLogLevel.DeveloperFiner);

        public static readonly LogLevel DeveloperVerbose = new LogLevel(PredefinedLogLevel.DeveloperVerbose);

        public static readonly LogLevel DeveloperFinest = new LogLevel(PredefinedLogLevel.DeveloperFinest);

        // 192 - 255 Custom
        // 0xC0 - 0xFF Custom
        public static readonly LogLevel Off = new LogLevel(PredefinedLogLevel.Off);
    }
}
