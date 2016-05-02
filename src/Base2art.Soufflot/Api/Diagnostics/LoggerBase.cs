namespace Base2art.Soufflot.Api.Diagnostics
{
    public abstract class LoggerBase : ILogger
    {
        private readonly LogLevel level;

        protected LoggerBase(LogLevel level)
        {
            this.level = level;
        }

        public void Log(string message, LogLevel messageLevel)
        {
            if (this.level.Level >= messageLevel.Level)
            {
                this.LogMesssage(new LogMessage(message, messageLevel));
            }
        }

        private class LogMessage : ILogMessage
        {
            private readonly string message;

            private readonly LogLevel messageLevel;

            public LogMessage(string message, LogLevel messageLevel)
            {
                this.message = message;
                this.messageLevel = messageLevel;
            }

            public LogLevel Level
            {
                get
                {
                    return this.messageLevel;
                }
            }

            public string Message
            {
                get
                {
                    return this.message;
                }
            }

            public override string ToString()
            {
                return string.Format("[{0}] {1}", this.Level.DisplayName, this.Message);
            }
        }

        protected abstract void LogMesssage(ILogMessage logMessage);
    }
}
