namespace Base2art.Soufflot.Api.Diagnostics
{
    using System;
    using System.IO;
    using Base2art.Validation;

    public class TextWriterLogger : LoggerBase
    {
        private readonly TextWriter logger;

        public TextWriterLogger(TextWriter logger, LogLevel level)
            : base(level)
        {
            logger.Validate().IsNotNull();
            this.logger = logger;
        }
        
        protected override void LogMesssage(ILogMessage logMessage)
        {
            this.logger.WriteLine(logMessage);
            this.logger.Flush();
        }
    }
}
