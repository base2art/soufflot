namespace Base2art.Soufflot.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public interface IRouteData<out T>
    {
        IClass<T> ControllerClass { get; }

        Type Type { get; }

        Expression<Func<IRenderingController, IHttpContext, List<PositionedResult>, IResult>> Expression { get; }
    }
}