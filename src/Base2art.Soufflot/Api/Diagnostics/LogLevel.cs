namespace Base2art.Soufflot.Api.Diagnostics
{
    public class LogLevel
    {
        private readonly byte logLevel;

        private readonly string name;

        private readonly string displayName;

        public LogLevel(PredefinedLogLevel predefinedLogLevel):
            this((byte)predefinedLogLevel, predefinedLogLevel.ToString("G"), predefinedLogLevel.ToString("G"))
        {
        }

        public LogLevel(byte logLevel, string name, string displayName)
        {
            this.logLevel = logLevel;
            this.name = name;
            this.displayName = displayName;
        }

        public byte Level
        {
            get
            {
                return this.logLevel;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string DisplayName
        {
            get
            {
                return this.displayName;
            }
        }
    }
}
