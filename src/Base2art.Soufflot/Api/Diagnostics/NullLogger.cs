namespace Base2art.Soufflot.Api.Diagnostics
{
    public class NullLogger : ILogger
    {
        public void Log(string message, LogLevel level)
        {
        }
    }
}