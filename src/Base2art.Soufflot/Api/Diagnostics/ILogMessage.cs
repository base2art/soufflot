namespace Base2art.Soufflot.Api.Diagnostics
{
    public interface ILogMessage
    {
        LogLevel Level { get; }

        string Message { get; }
    }
}
