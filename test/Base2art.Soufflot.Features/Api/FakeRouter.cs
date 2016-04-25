namespace Base2art.Soufflot.Api
{
    using System.Collections.Generic;
    using System.Linq;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Linq;
    using Base2art.Soufflot.Mvc;

    public class FakeRouter
    {
        public static IRouter Create(IClass<IRenderingController> klazz, params IClass<INonRenderingController>[] otherKlazzez)
        {
            return new TempRouter(klazz, otherKlazzez.Coalesce());
        }

        public static IRouter CreateData(IRouteData<IRenderingController> routeData)
        {
            return new TempRouter(routeData);
        }

        private class TempRouter : IRouter
        {
            private readonly IRouteData<IRenderingController> routeData;

            private readonly IClass<IRenderingController> klazz;

            private readonly IEnumerable<IClass<INonRenderingController>> otherKlazzez;

            public TempRouter(IRouteData<IRenderingController> routeData)
            {
                this.routeData = routeData;
                this.otherKlazzez = new IClass<INonRenderingController>[0];
            }

            public TempRouter(IClass<IRenderingController> klazz, IEnumerable<IClass<INonRenderingController>> otherKlazzez)
            {
                this.klazz = klazz;
                this.otherKlazzez = otherKlazzez;
            }

            public IRouteData<IRenderingController> FindRenderingControllerType(IHttpRequest request)
            {
                if (this.routeData != null)
                {
                    return this.routeData;
                }

                if (this.klazz == null)
                {
                    return null;
                }

                if (this.klazz.Type == typeof(INotPossibleController))
                {
                    return new FakeRouteData<IRenderingController>(null);
                }

                return new FakeRouteData<IRenderingController>(this.klazz);
            }

            public IEnumerable<IRouteData<INonRenderingController>> FindNonRenderingControllerTypes(IHttpRequest request)
            {
                return this.otherKlazzez.Select(x => new FakeRouteData<INonRenderingController>(x));
            }
        }
    }
}