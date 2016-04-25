namespace Base2art.Soufflot.Mvc
{
    using Base2art.Soufflot.Http;

    public interface INonRenderingController
    {
        INonRenderingController[] NonRenderingControllers { get; }

        void Execute(IHttpContext httpContext);
    }
}