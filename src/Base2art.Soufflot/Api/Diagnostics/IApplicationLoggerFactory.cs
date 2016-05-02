namespace Base2art.Soufflot.Api.Diagnostics
{
    public interface IApplicationLoggerFactory
    {
        ILogger Create(LogLevel logLevel);
    }
}
