namespace Base2art.Soufflot.Api
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Http;

    public sealed class NullRouter : IRouter
    {
        public IRouteData<IRenderingRouted> FindRenderingRoutedType(IHttpRequest request)
        {
            return null;
        }

        public IEnumerable<IRouteData<INonRenderingRouted>> FindNonRenderingRoutedTypes(IHttpRequest request)
        {
            return null;
        }
    }
}
