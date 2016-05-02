namespace Base2art.Soufflot.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Base2art.Soufflot.Http;

    public interface IRouteData<out T>
    {
        IClass<T> RoutedClass { get; }

        Type Type { get; }

        Expression<Func<IRenderingRouted, IHttpContext, List<PositionedResult>, IResult>> Expression { get; }
    }
}