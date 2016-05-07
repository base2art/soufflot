namespace Base2art.Soufflot.Pack.Features
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Base2art.Soufflot.Api;
    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class FakeRouteData<T> : IRouteData<T>
    {
        private readonly IClass<T> klazz;

        private readonly Expression<Func<IRenderingRouted, IHttpContext, List<PositionedResult>, IResult>> expr;

        public FakeRouteData(IClass<T> klazz)
            : this(klazz, null)
        {
        }

        public FakeRouteData(IClass<T> klazz, Expression<Func<IRenderingRouted, IHttpContext, List<PositionedResult>, IResult>> expr)
        {
            this.klazz = klazz;
            this.expr = expr;
        }

        public IClass<T> ControllerClass
        {
            get { return this.klazz; }
        }

        public Type Type
        {
            get { return this.ControllerClass.Type; }
        }

        public Expression<Func<IRenderingRouted, IHttpContext, List<PositionedResult>, IResult>> Expression
        {
            get { return this.expr; }
        }
    }
}
