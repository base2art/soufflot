namespace Base2art.Soufflot.Api.Diagnostics
{
    public interface ILogger
    {
        void Log(string message, LogLevel level);
    }
}
