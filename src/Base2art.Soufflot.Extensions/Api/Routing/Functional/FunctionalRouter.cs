namespace Base2art.Soufflot.Api.Routing.Functional
{
    using System.Collections.Generic;

    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Linq;
    using Base2art.Soufflot.Mvc;

    public class FunctionalRouter : IRouter
    {
        private readonly IRenderingControllerSearchDelegate[] renderingControllerSearchDelegates;

        private readonly INonRenderingControllerSearchDelegate[] nonRenderingControllerSearchDelegates;

        public FunctionalRouter(
            IRenderingControllerSearchDelegate[] renderingControllerSearchDelegates,
            INonRenderingControllerSearchDelegate[] nonRenderingControllerSearchDelegates)
        {
            this.renderingControllerSearchDelegates = renderingControllerSearchDelegates;
            this.nonRenderingControllerSearchDelegates = nonRenderingControllerSearchDelegates;
        }

        public IRouteData<IRenderingController> FindRenderingControllerType(IHttpRequest request)
        {
            foreach (var routeFindingDelegate in this.renderingControllerSearchDelegates.Coalesce())
            {
                var rez = routeFindingDelegate.FindType(request);
                if (rez != null)
                {
                    return new FunctionalRouteData<IRenderingController>(rez.GetClass().As<IRenderingController>());
                }
            }

            return null;
        }

        public IEnumerable<IRouteData<INonRenderingController>> FindNonRenderingControllerTypes(IHttpRequest request)
        {
            foreach (var routeFindingDelegate in this.nonRenderingControllerSearchDelegates.Coalesce())
            {
                var rez = routeFindingDelegate.FindType(request);
                if (rez != null)
                {
                    yield return new FunctionalRouteData<INonRenderingController>(rez.GetClass().As<INonRenderingController>());
                }
            }
        }
    }
}
