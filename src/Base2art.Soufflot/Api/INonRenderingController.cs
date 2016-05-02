namespace Base2art.Soufflot.Api
{
    using Base2art.Soufflot.Http;

    public interface INonRenderingRouted
    {
        INonRenderingRouted[] NonRenderingRoutedItems { get; }

        void Execute(IHttpContext httpContext);
    }
}
