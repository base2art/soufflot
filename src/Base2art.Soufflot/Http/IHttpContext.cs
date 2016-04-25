namespace Base2art.Soufflot.Http
{
    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Api.Diagnostics;

    public interface IHttpContext
    {
        IApplication ApplicationInstance { get; }

        ILogger Logger { get; }

        IHttpFlash Flash { get; }
        
        IHttpSession Session { get; }
 
        IHttpResponse Response { get; }

        IHttpRequest Request { get; }
    }
}