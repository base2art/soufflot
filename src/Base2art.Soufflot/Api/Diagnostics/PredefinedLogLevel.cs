namespace Base2art.Soufflot.Api.Diagnostics
{
    public enum PredefinedLogLevel : byte
    {
        Off = 0x00,

        // 1-63
        // 0x01 - 0x3F
        SystemEmergencyEvent = 0x05,
        SystemFatalEvent = 0x0A,
        SystemAlertEvent = 0x12,
        SystemCriticalEvent = 0x17,
        SystemSevereEvent = 0x1B,

        // 64 - 127
        // 0x40 - 0x7F
        ApplicationError = 0x45,
        ApplicationWarn = 0x4A,
        ApplicationDebug = 0x52,
        ApplicationNotice = 0x57,
        ApplicationInfo = 0x5B,

        // 128 - 191
        // 0x80 - 0xBF
        DeveloperTrace = 0x86,
        DeveloperFiner = 0x8C,
        DeveloperVerbose = 0x94,
        DeveloperFinest = 0x9e,

        // 192 - 255 Custom
        // 0xC0 - 0xFF Custom

        Always = 0xFF
    }
}
