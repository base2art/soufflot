namespace Base2art.Soufflot.Api.Diagnostics
{
    using System.Collections.Generic;

    public class InMemoryLogger : LoggerBase
    {
        private readonly List<ILogMessage> messages = new List<ILogMessage>();

        public InMemoryLogger(LogLevel level)
            : base(level)
        {
        }

        protected override void LogMesssage(ILogMessage logMessage)
        {
            messages.Add(logMessage);
        }

        public ILogMessage[] Messages
        {
            get
            {
                return this.messages.ToArray();
            }
        }
    }
}
