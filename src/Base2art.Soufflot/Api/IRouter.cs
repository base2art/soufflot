namespace Base2art.Soufflot.Api
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Http;

    public interface IRouter
    {
        IRouteData<IRenderingRouted> FindRenderingRoutedType(IHttpRequest request);

        IEnumerable<IRouteData<INonRenderingRouted>> FindNonRenderingRoutedTypes(IHttpRequest request);
    }
}
