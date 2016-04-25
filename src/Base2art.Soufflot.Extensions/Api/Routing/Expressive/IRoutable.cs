namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public interface IRoutable<T> : IRoute
    {
        IRoutable<T> With(Expression<Func<T, IHttpContext, List<PositionedResult>, IResult>> expr);

        IRoutable<T> With<TInput1>(Expression<Func<T, IHttpContext, List<PositionedResult>, TInput1, IResult>> expr);

        bool HasValue { get; }

        IRoutable<T> OnDomain(string domain);
    }
}