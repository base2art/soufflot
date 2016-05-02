namespace Base2art.Soufflot.Api.Diagnostics
{
    using System;
    public class ConsoleLoggerFactory : IRequestLoggerFactory, IApplicationLoggerFactory
    {
        public ILogger Create(LogLevel logLevel)
        {
            return new TextWriterLogger(Console.Out, logLevel);
        }
    }
}
