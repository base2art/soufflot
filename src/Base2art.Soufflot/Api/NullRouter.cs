namespace Base2art.Soufflot.Api
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public sealed class NullRouter : IRouter
    {
        public IRouteData<IRenderingController> FindRenderingControllerType(IHttpRequest request)
        {
            return null;
        }

        public IEnumerable<IRouteData<INonRenderingController>> FindNonRenderingControllerTypes(IHttpRequest request)
        {
            return null;
        }
    }
}