namespace Base2art.Soufflot.Api.Routing
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class RouteData<T> : IRouteData<T>
    {
        private readonly IClass<T> controllerClass;

        private readonly Expression<Func<IRenderingRouted, IHttpContext, List<PositionedResult>, IResult>> expression;

        public RouteData(
            IClass<T> controllerClass, 
            Expression<Func<IRenderingRouted, IHttpContext, List<PositionedResult>, IResult>> expression)
        {
            this.controllerClass = controllerClass;
            this.expression = expression;
        }

        public IClass<T> ControllerClass
        {
            get
            {
                return this.controllerClass;
            }
        }

        public Type Type
        {
            get
            {
                return this.ControllerClass.Type;
            }
        }

        public Expression<Func<IRenderingRouted, IHttpContext, List<PositionedResult>, IResult>> Expression
        {
            get
            {
                return this.expression;
            }
        }
    }
}