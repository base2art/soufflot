namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class ExpressiveRouteData<T> : RouteData<T>
    {
        public ExpressiveRouteData(IClass<T> controllerClass, Expression<Func<IRenderingRouted, IHttpContext, List<PositionedResult>, IResult>> expression)
            : base(controllerClass, expression)
        {
        }
    }
}