namespace Base2art.Soufflot.Api.Diagnostics
{
	public interface IRequestLoggerFactory
	{
		ILogger Create(LogLevel logLevel);
	}
}


