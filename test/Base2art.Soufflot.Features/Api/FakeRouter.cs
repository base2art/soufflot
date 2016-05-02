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
        public static IRouter Create(IClass<IRenderingRouted> klazz, params IClass<INonRenderingRouted>[] otherKlazzez)
        {
            return new TempRouter(klazz, otherKlazzez.Coalesce());
        }

        public static IRouter CreateData(IRouteData<IRenderingRouted> routeData)
        {
            return new TempRouter(routeData);
        }

        private class TempRouter : IRouter
        {
            private readonly IRouteData<IRenderingRouted> routeData;

            private readonly IClass<IRenderingRouted> klazz;

            private readonly IEnumerable<IClass<INonRenderingRouted>> otherKlazzez;

            public TempRouter(IRouteData<IRenderingRouted> routeData)
            {
                this.routeData = routeData;
                this.otherKlazzez = new IClass<INonRenderingRouted>[0];
            }

            public TempRouter(IClass<IRenderingRouted> klazz, IEnumerable<IClass<INonRenderingRouted>> otherKlazzez)
            {
                this.klazz = klazz;
                this.otherKlazzez = otherKlazzez;
            }

            public IRouteData<IRenderingRouted> FindRenderingControllerType(IHttpRequest request)
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
                    return new FakeRouteData<IRenderingRouted>(null);
                }

                return new FakeRouteData<IRenderingRouted>(this.klazz);
            }

            public IEnumerable<IRouteData<INonRenderingRouted>> FindNonRenderingControllerTypes(IHttpRequest request)
            {
                return this.otherKlazzez.Select(x => new FakeRouteData<INonRenderingRouted>(x));
            }
        }
    }
}