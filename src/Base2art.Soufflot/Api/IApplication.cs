namespace Base2art.Soufflot.Api
{
    
	using Base2art.Soufflot.Api.Diagnostics;

    using Base2art.Soufflot.Http;

    public interface IApplication
    {
        string RootDirectory { get; }
        
        ILogger ApplicationLogger { get; }

        T CreateInstance<T>(IClass<T> createInstanceClass, bool returnNullOnErrorOrNotFound)
            where T : class;

        T[] CreateInstances<T>(IClass<T> createInstanceClass, bool returnNullOnErrorOrNotFound)
                where T : class;

        IResult OnRouteNotFound(IHttpContext httpContext);

        IRouter CreateRouter();

        string ConfigurationValue(string key);

        ApplicationMode Mode { get; }
    }
}