namespace Base2art.Soufflot.Api
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Mvc;

    using Base2art.Soufflot.Http;

    public interface IRouter
    {
        IRouteData<IRenderingController> FindRenderingControllerType(IHttpRequest request);

        IEnumerable<IRouteData<INonRenderingController>> FindNonRenderingControllerTypes(IHttpRequest request);
    }
}