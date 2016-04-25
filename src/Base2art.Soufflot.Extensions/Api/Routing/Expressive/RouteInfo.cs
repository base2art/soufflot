namespace Base2art.Soufflot.Api.Routing.Expressive
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;

    using Base2art.Soufflot.Http;
    using Base2art.Soufflot.Mvc;

    public class RouteInfo
    {
        public HttpMethod? Method { get; set; }

        public string Host { get; set; }

        public string Path { get; set; }

        public Regex PathMatcher { get; set; }

        public Type Type { get; set; }

        public Expression Expression { get; set; }

        public RouteExpressionTree ExpressionTree { get; set; }

        public Expression<Func<IRenderingController, IHttpContext, List<PositionedResult>, IResult>>
            WrapperExpression { get; set; }
    }
}